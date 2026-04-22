using System.Linq.Expressions;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 公共业务逻辑
/// </summary>
/// <remarks>继承公共仓储，为业务逻辑层提供方法</remarks>
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