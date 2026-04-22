using System.Dynamic;
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
    /// 根据查询参数获取对应的所有旅游线路
    /// </summary>
    /// <param name="routeParams">路线参数</param>
    /// <param name="paginationParams">分页参数</param>
    /// <returns>所有旅游路线</returns>
    Task<(IEnumerable<ExpandoObject>, object)> GetAllRoutesAsync(TouristRouteResourceParameters routeParams,
        PaginationResourceParameters paginationParams);

    /// <summary>
    /// 根据路线id从获取对应的单个旅游路线
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="fields"></param>
    /// <returns>单个旅游路线</returns>
    Task<ExpandoObject> GetRouteByIdAsync(Guid routeId, string fields);

    /// <summary>
    /// 根据路线id获取需要进行局部更新的旅游路线
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>返回需要进行局部更新的旅游路线</returns>
    Task<TouristRouteUpdateDto> GetPartialUpdateRouteByIdAsync(Guid routeId);

    /// <summary>
    /// 创建旅游路线
    /// </summary>
    /// <param name="routeCreateDto">旅游路线创建DTO</param>
    /// <returns></returns>
    Task<TouristRouteDto> CreateRouteAsync(TouristRouteCreateDto routeCreateDto);

    /// <summary>
    /// 根据路线id对指定旅游路线进行更新
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="routeUpdateDto">路线更新DTO</param>
    Task UpdateRouteAsync(Guid routeId, TouristRouteUpdateDto routeUpdateDto);

    /// <summary>
    /// 根据路线id对指定路线进行局部更新
    /// </summary>
    /// <param name="routeId">路线id</param>
    Task PartialUpdateRouteAsync(Guid routeId);

    /// <summary>
    /// 根据路线id删除指定旅游路线
    /// </summary>
    /// <param name="routeId">路线id</param>
    Task DeleteRouteAsync(Guid routeId);

    /// <summary>
    /// 根据路线id集合删除旅游路线列表
    /// </summary>
    /// <param name="routeIds">路线id集合</param>
    Task DeleteRoutesAsync(IEnumerable<Guid> routeIds);

    /// <summary>
    /// 根据传入字段判断映射参数是否存在
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns>存在返回true，反之返回false</returns>
    bool MappingExists(string fields);

    /// <summary>
    /// 根据传入字段判断属性是否存在
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns>存在返回true，反之返回false</returns>
    bool PropertiesExists(string fields);
}