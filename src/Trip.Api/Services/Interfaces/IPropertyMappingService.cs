using Trip.Api.Mappers.PropertyMappings;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 属性映射业务逻辑
/// </summary>
public interface IPropertyMappingService
{
    /// <summary>
    /// 获取源类型与目标类型之间的属性映射关系
    /// </summary>
    /// <typeparam name="TSource">原数据</typeparam>
    /// <typeparam name="TDestination">目标数据</typeparam>
    /// <returns>完成属性映射操作后得到的键值对</returns>
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();

    /// <summary>
    /// 根据传入字段判断映射参数是否存在
    /// </summary>
    /// <param name="fields">传入字段</param>
    /// <typeparam name="TSource">原数据</typeparam>
    /// <typeparam name="TDestination">目标数据</typeparam>
    /// <returns>存在返回true，反之返回false</returns>
    bool IsMappingExists<TSource, TDestination>(string? fields);

    /// <summary>
    /// 根据传入字段判断属性是否存在
    /// </summary>
    /// <param name="fields">传入字段</param>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <returns>存在返回true，反之返回false</returns>
    bool IsPropertyExists<T>(string? fields);
}