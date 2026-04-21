using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 旅游路线图片服务
/// </summary>
public interface ITouristRoutePictureService : ICommonService<TouristRoutePicture>
{
    Task<IEnumerable<TouristRoutePictureDto>> GetAllPicturesByRouteIdAsync(Guid routeId);

    Task<TouristRoutePictureDto> GetPictureByIdAsync(int pictureId);

    Task<TouristRoutePictureDto> CreateTouristRoutePictureAsync(Guid routeId,
        TouristRoutePictureCreateDto pictureCreateDto);

    Task UpdatePictureByIdAsync(int pictureId, TouristRoutePictureUpdateDto pictureUpdateDto);

    Task DeletePictureByIdAsync(int pictureId);

    Task DeletePicturesByIdsAsync(IEnumerable<int> pictureIds);
}