using System.Linq.Expressions;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 公共仓储服务
/// </summary>
/// <remarks>为仓储服务提供公共方法</remarks>
public interface ICommonService<T> where T : class
{
    /// <summary>
    /// 根据实体id判断实体数据是否存在
    /// </summary>
    /// <param name="entityId">实体id</param>
    /// <returns>存在返回true，反之返回false</returns>
    Task<bool> CheckExitsAsync(Expression<Func<T, bool>> entityId);

    /// <summary>
    /// 判断是否将数据插入到数据库
    /// </summary>
    /// <returns>插入成功返回true，失败返回false</returns>
    Task<bool> SaveAsync();
}