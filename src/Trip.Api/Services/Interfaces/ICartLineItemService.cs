using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 购物车商品业务逻辑
/// </summary>
public interface ICartLineItemService : ICommonService<CartLineItem>
{
    /// <summary>
    /// 创建商品
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <param name="itemCreateDto">创建商品DTO</param>
    /// <returns>创建完成，将生成数据展示出来</returns>
    Task<CartLineItemDto> CreateItemAsync(string userId, CartLineItemCreateDto itemCreateDto);

    /// <summary>
    /// 删除商品
    /// </summary>
    /// <param name="itemId">商品id</param>
    Task DeleteItemAsync(int itemId);

    /// <summary>
    /// 删除商品列表
    /// </summary>
    /// <param name="itemIds">商品id集合</param>
    Task DeleteItemsAsync(IEnumerable<int> itemIds);
}