using System.Linq.Expressions;

namespace Trip.Api.Repositories.Interfaces;

/// <summary>
/// 公共仓储
/// </summary>
/// <remarks>为仓储层提供公共方法</remarks>
public interface ICommonRepository<T> where T : class
{
    /// <summary>
    /// 根据实体id从数据数据获取相对应实体数据
    /// </summary>
    /// <param name="entityId">实体id</param>
    /// <returns>获取到实体返回true，反之返回false</returns>
    Task<bool> CheckExitsAsync(Expression<Func<T, bool>> entityId);

    /// <summary>
    /// 判断是否将数据插入到数据库
    /// </summary>
    /// <returns>插入成功返回true，失败返回false</returns>
    Task<bool> SaveAsync();
}