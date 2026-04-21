using Trip.Api.Entities;

namespace Trip.Api.Repositories.Interfaces;

/// <summary>
/// 旅游路线图片仓储服务
/// </summary>
public interface ITouristRoutePictureRepository : ICommonRepository<TouristRoutePicture>
{
    /// <summary>
    /// 根据路线id从数据库中获取该路线下的所有图片实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <returns>所有图片</returns>
    internal Task<IList<TouristRoutePicture>> GetAllPicturesByRouteIdAsync(Guid routeId);

    /// <summary>
    /// 根据图片id从数据库中获取对应的单个图片实体数据
    /// </summary>
    /// <param name="pictureId">图片id</param>
    /// <returns>单个图片</returns>
    internal Task<TouristRoutePicture> GetPictureByIdAsync(int pictureId);

    /// <summary>
    /// 根据图片id集合从数据库中获取图片列表
    /// </summary>
    /// <param name="pictureIds">图片id</param>
    /// <returns>图片列表</returns>
    internal Task<IEnumerable<TouristRoutePicture>> GetPicturesByIdsAsync(IEnumerable<int> pictureIds);

    /// <summary>
    /// 添加旅游路线图片实体数据
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="picture">旅游路线图片</param>
    internal Task CreatePictureAsync(Guid routeId, TouristRoutePicture picture);

    /// <summary>
    /// 删除旅游路线图片实体数据
    /// </summary>
    /// <param name="picture">旅游路线图片</param>
    internal void DeletePicture(TouristRoutePicture picture);

    /// <summary>
    /// 删除旅游路线图片列表实体数据
    /// </summary>
    /// <param name="pictures">旅游路线图片列表</param>
    internal void DeletePictures(IEnumerable<TouristRoutePicture> pictures);
}