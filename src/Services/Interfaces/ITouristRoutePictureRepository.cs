using Trip.Api.Models;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线图片仓储服务
/// </summary>
public interface ITouristRoutePictureRepository : ICommonRepository
{
    /// <summary>
    /// 根据对应的路线id获取该路线下的全部图片
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>该路线下的所有图片</returns>
    IEnumerable<TouristRoutePicture> GetAllPicturesByRouteId(Guid routeId);

    /// <summary>
    /// 根据图片id获取对应的图片
    /// </summary>
    /// <param name="pictureId">图片id</param>
    /// <returns>图片id对应的图片</returns>
    TouristRoutePicture GetPictureById(int pictureId);

    /// <summary>
    /// 在路线id对应的路线中添加单个图片
    /// </summary>
    /// <param name="routeId"></param>
    /// <param name="picture"></param>
    void AddPicture(Guid routeId, TouristRoutePicture picture);
}