using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线图片仓储服务
/// </summary>
public interface ITouristRoutePictureRepository : ICommonRepository
{
    /// <summary>
    /// 根据路线id从数据库中获取该路线下的所有图片实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>所有图片</returns>
    Task<IList<TouristRoutePicture>> GetAllPicturesByRouteIdAsync(Guid routeId);

    /// <summary>
    /// 根据图片id从数据库中获取对应的单个图片实体数据
    /// </summary>
    /// <param name="pictureId">图片id</param>
    /// <returns>单个图片</returns>
    Task<TouristRoutePicture> GetRouteByIdAsync(int pictureId);

    /// <summary>
    /// 添加旅游路线图片实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="picture">旅游路线图片</param>
    Task AddPictureAsync(Guid routeId, TouristRoutePicture picture);
}