using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线图片服务
/// </summary>
public interface ITouristRoutePictureService : ICommonService<TouristRoutePicture>
{
    /// <summary>
    /// 根据路线id返回对应路线下所有的图片
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>所有的图片</returns>
    Task<IEnumerable<TouristRoutePictureDto>> GetAllPicturesByRouteIdAsync(Guid routeId);

    /// <summary>
    /// 根据图片id获取对应的图片
    /// </summary>
    /// <param name="pictureId">图片id</param>
    /// <returns>对应的图片</returns>
    Task<TouristRoutePictureDto> GetPictureByIdAsync(int pictureId);

    /// <summary>
    /// 根据路线id创建该旅游路线下的新图片
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="pictureCreateDto">图片创建DTO</param>
    /// <returns>创建完成返回数据</returns>
    Task<TouristRoutePictureDto> CreatePictureAsync(Guid routeId,
        TouristRoutePictureCreateDto pictureCreateDto);

    /// <summary>
    /// 根据图片id更新该旅游路线下的图片
    /// </summary>
    /// <param name="pictureId">图片id</param>
    /// <param name="pictureUpdateDto">图片更新DTO</param>
    /// <returns>更新完成返回数据</returns>
    Task UpdatePictureByIdAsync(int pictureId, TouristRoutePictureUpdateDto pictureUpdateDto);

    /// <summary>
    /// 根据图片id删除对应图片
    /// </summary>
    /// <param name="pictureId">图片id</param>
    Task DeletePictureByIdAsync(int pictureId);

    /// <summary>
    /// 根据图片id集合删除图片集合
    /// </summary>
    /// <param name="pictureIds">图片id集合</param>
    Task DeletePicturesByIdsAsync(IEnumerable<int> pictureIds);
}