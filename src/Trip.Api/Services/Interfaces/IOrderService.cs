using Trip.Api.Dtos.Order;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.ResourceParameters;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 订单业务逻辑
/// </summary>
public interface IOrderService : ICommonRepository<Order>
{
    /// <summary>
    /// 获取所有订单
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <param name="paginationParams">分页参数</param>
    /// <returns>所有订单数据</returns>
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string userId, PaginationResourceParameters paginationParams);

    /// <summary>
    /// 根据订单id获取对应订单
    /// </summary>
    /// <param name="orderId">订单id</param>
    /// <returns>对应订单数据</returns>
    Task<OrderDto> GetOrderAsync(Guid orderId);

    /// <summary>
    /// 根据订单id对相应订单进行支付操作
    /// </summary>
    /// <param name="orderId">订单id</param>
    /// <returns>将完成支付操作的订单展示</returns>
    Task<OrderDto> PlaceOrderAsync(Guid orderId);
}