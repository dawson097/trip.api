using Trip.Api.Entities;
using Trip.Api.Helpers;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线仓储服务
/// </summary>
public interface ITouristRouteRepository : ICommonRepository
{
    /// <summary>
    /// 从数据库中根据条件获取与条件匹配的所有旅游路线实体数据
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="ratingType">评价类型</param>
    /// <param name="ratingValue">评价值</param>
    /// <param name="pageSize">每页显示数据量</param>
    /// <param name="pageNumber">页码</param>
    /// <returns>所有旅游路线</returns>
    Task<PaginationHelper<TouristRoute>> GetAllRoutesAsync(string keyword, string ratingType, int? ratingValue,
        int pageSize,
        int pageNumber);

    /// <summary>
    /// 根据路线id从数据库获取对应的单个旅游路线实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>单个旅游路线</returns>
    Task<TouristRoute> GetRouteByIdAsync(Guid routeId);

    /// <summary>
    /// 根据类型id集合获取旅游路线列表实体数据
    /// </summary>
    /// <param name="routeIds">路线id集合</param>
    /// <returns>旅游路线列表</returns>
    Task<IEnumerable<TouristRoute>> GetRoutesByIdsAsync(IEnumerable<Guid> routeIds);

    /// <summary>
    /// 添加旅游路线实体数据
    /// </summary>
    /// <param name="route">旅游路线实体</param>
    Task AddRouteAsync(TouristRoute route);

    /// <summary>
    /// 删除旅游路线实体数据
    /// </summary>
    /// <param name="route">旅游路线实体</param>
    void DeleteRoute(TouristRoute route);

    /// <summary>
    /// 删除旅游路线列表实体数据
    /// </summary>
    /// <param name="routes">旅游路线列表</param>
    void DeleteRoutes(IEnumerable<TouristRoute> routes);
}