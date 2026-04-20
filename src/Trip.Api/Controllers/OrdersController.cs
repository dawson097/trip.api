using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trip.Api.Dtos.Order;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 订单控制器路由
/// </summary>
[ApiController, Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, IOrderRepository orderRepository)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    [HttpGet, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] PaginationResourceParameter paginationParams)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var orders =
            await _orderRepository.GetAllOrdersByUserIdAsync(userId, paginationParams.PageSize,
                paginationParams.PageNumber);

        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }


    [HttpGet("{orderId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrderAsync(Guid orderId)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpPost("{orderId:guid}/place-order"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PlaceOrderAsync([FromRoute] Guid orderId)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        // 订单支付处理
        order.PaymentProcessing();
        await _orderRepository.SaveAsync();

        var httpClient = _httpClientFactory.CreateClient("OrderApi");
        var url = @"http://localhost:8080/api/payment-process?&orderNumber={0}&returnFault={1}";

        var resp = await httpClient.PostAsync(string.Format(url, order.Id, false), null);

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
            order.PaymentApproved();
        }
        else
        {
            // 支付失败
            order.PaymentRejected();
        }

        order.TransactionMetadata = transactionMetadata;
        await _orderRepository.SaveAsync();

        return Ok(_mapper.Map<OrderDto>(order));
    }
}