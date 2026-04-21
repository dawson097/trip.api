using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Entities;
using Trip.Api.ResourceParameters;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线服务
/// </summary>
public interface ITouristRouteService : ICommonService<TouristRoute>
{
    /// <summary>
    /// 从数据库中根据条件获取与条件匹配的所有旅游路线实体数据
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="ratingType">评价类型</param>
    /// <param name="ratingValue">评价值</param>
    /// <param name="pageSize">每页显示数据条数</param>
    /// <param name="pageNumber">当前请求页面</param>
    /// <returns>所有旅游路线</returns>
    Task<(List<TouristRouteDto>, object)> GetAllRoutesAsync(TouristRouteResourceParameter routeParams,
        PaginationResourceParameter paginationParamsr);

    /// <summary>
    /// 根据路线id从数据库获取对应的单个旅游路线实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>单个旅游路线</returns>
    Task<TouristRouteDto> GetRouteByIdAsync(Guid routeId);

    Task<TouristRouteUpdateDto> GetPartialRouteByIdAsync(Guid routeId);

    /// <summary>
    /// 添加旅游路线实体数据
    /// </summary>
    /// <param name="route">旅游路线实体</param>
    Task<TouristRouteDto> CreateRouteAsync(TouristRouteCreateDto routeCreateDto);

    Task UpdateRouteAsync(Guid routeId, TouristRouteUpdateDto routeUpdateDto);

    Task PartialUpdateRouteAsync(Guid routeId);

    /// <summary>
    /// 删除旅游路线实体数据
    /// </summary>
    /// <param name="route">旅游路线实体</param>
    Task DeleteRouteAsync(Guid routeId);

    /// <summary>
    /// 删除旅游路线列表实体数据
    /// </summary>
    /// <param name="routes">旅游路线列表</param>
    Task DeleteRoutesAsync(IEnumerable<Guid> routeIds);
}