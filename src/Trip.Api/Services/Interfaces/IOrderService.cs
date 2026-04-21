using Trip.Api.Dtos.Order;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.ResourceParameters;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 订单仓储服务
/// </summary>
public interface IOrderService : ICommonRepository<Order>
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string userId, PaginationResourceParameter paginationParams);

    Task<OrderDto> GetOrderAsync(Guid routeId);

    Task<OrderDto> PlaceOrderAsync(Guid routeId);
}