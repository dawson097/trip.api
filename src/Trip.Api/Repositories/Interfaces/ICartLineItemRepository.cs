using Trip.Api.Entities;

namespace Trip.Api.Repositories.Interfaces;

public interface ICartLineItemRepository : ICommonRepository<CartLineItem>
{
    /// <summary>
    /// 根据商品id集合从数据库中批量获取商品列表实体数据
    /// </summary>
    /// <param name="itemIds">商品列表id集合</param>
    /// <returns>商品列表</returns>
    Task<IEnumerable<CartLineItem>> GetItemsByIdsAsync(IEnumerable<int> itemIds);

    /// <summary>
    /// 根据商品id从数据库中获取对应的单个商品实体数据
    /// </summary>
    /// <param name="itemId">商品id</param>
    /// <returns>对应的单个商品</returns>
    Task<CartLineItem> GetItemByIdAsync(int itemId);

    /// <summary>
    /// 创建单个商品实体数据
    /// </summary>
    /// <param name="item">单个</param>
    Task CreateItemAsync(CartLineItem item);

    /// <summary>
    /// 删除单个商品实体数据
    /// </summary>
    /// <param name="item">商品实体</param>
    void DeleteItem(CartLineItem item);

    /// <summary>
    /// 删除商品列表实体数据
    /// </summary>
    /// <param name="items">商品列表</param>
    void DeleteItems(IEnumerable<CartLineItem> items);
}