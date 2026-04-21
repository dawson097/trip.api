using MapsterMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trip.Api.Dtos.Order;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class OrderService(
    ICommonRepository<Order> commonRepository,
    IOrderRepository orderRepository,
    IMapper mapper,
    IHttpClientFactory httpClientFactory)
    : CommonService<Order>(commonRepository), IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string userId,
        PaginationResourceParameter paginationParams)
    {
        var ordersFromRepo =
            await orderRepository.GetAllOrdersByUserIdAsync(userId, paginationParams.PageSize,
                paginationParams.PageNumber);

        return mapper.Map<IEnumerable<OrderDto>>(ordersFromRepo);
    }

    public async Task<OrderDto> GetOrderAsync(Guid routeId)
    {
        var order = await orderRepository.GetOrderByIdAsync(routeId);

        return mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> PlaceOrderAsync(Guid orderId)
    {
        var orderFromRepo = await orderRepository.GetOrderByIdAsync(orderId);

        orderFromRepo.PaymentProcessing();
        await orderRepository.SaveAsync();

        var httpClient = httpClientFactory.CreateClient("OrderApi");
        var url = @"http://localhost:8080/api/payment-process?&orderNumber={0}&returnFault={1}";

        var resp = await httpClient.PostAsync(string.Format(url, orderFromRepo.Id, false), null);

        // 提取支付结果
        var isApproved = false;
        var transactionMetadata = "";

        if (resp.IsSuccessStatusCode)
        {
            transactionMetadata = await resp.Content.ReadAsStringAsync();
            var jsonObj = (JObject)JsonConvert.DeserializeObject(transactionMetadata)!;
            isApproved = jsonObj["approved"]!.Value<bool>();
        }

        if (isApproved)
        {
            // 支付成功，完成订单
            orderFromRepo.PaymentApproved();
        }
        else
        {
            // 支付失败
            orderFromRepo.PaymentRejected();
        }

        orderFromRepo.TransactionMetadata = transactionMetadata;
        await orderRepository.SaveAsync();

        return mapper.Map<OrderDto>(orderFromRepo);
    }
}