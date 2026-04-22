using Trip.Api.Mappers.PropertyMappings;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 属性映射业务逻辑
/// </summary>
public interface IPropertyMappingService
{
    /// <summary>
    /// 将原数据和目标数据进行属性映射
    /// </summary>
    /// <typeparam name="TSource">原数据</typeparam>
    /// <typeparam name="TDestination">目标数据</typeparam>
    /// <returns>完成属性映射操作后得到的键值对</returns>
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
}