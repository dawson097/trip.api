using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线仓储服务
/// </summary>
public interface ITouristRouteRepository : ICommonRepository
{
    /// <summary>
    /// 从数据库中获取所有旅游路线实体数据
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="ratingType">评价类型</param>
    /// <param name="ratingValue">评价值</param>
    /// <returns>所有旅游路线</returns>
    Task<IList<TouristRoute>> GetAllRoutesAsync(string keyword, string ratingType, int? ratingValue);

    /// <summary>
    /// 根据路线id从数据库获取对应的单个旅游路线实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>单个旅游路线</returns>
    Task<TouristRoute> GetRouteByIdAsync(Guid routeId);
}