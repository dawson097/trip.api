using Trip.Api.Models;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线仓储服务
/// </summary>
public interface ITouristRouteRepository : ICommonRepository
{
    /// <summary>
    /// 获取所有旅游路线
    /// </summary>
    /// <returns>所有的旅游路线</returns>
    IEnumerable<TouristRoute> GetAllRoutes();

    /// <summary>
    /// 根据路线id获取对应的旅游路线
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>对应的旅游路线</returns>
    TouristRoute GetRouteById(Guid routeId);
}