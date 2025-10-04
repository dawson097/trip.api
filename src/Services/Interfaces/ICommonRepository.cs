namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 公用仓储服务
/// </summary>
public interface ICommonRepository
{
    /// <summary>
    /// 根据路线id判断旅游路线是否存在
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>存在返回true，不存在返回false</returns>
    bool RouteExist(Guid routeId);
}