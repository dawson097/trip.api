namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 公共仓储服务
/// </summary>
/// <remarks>为仓储服务提供公共方法</remarks>
public interface ICommonRepository
{
    /// <summary>
    /// 根据路线id判断旅游路线实体数据是否存在
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>存在返回true，反之返回false</returns>
    Task<bool> RoutesExitsAsync(Guid routeId);
}