using Mapster;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Entities;

namespace Trip.Api.Mappers;

/// <summary>
/// 旅游路线图片映射
/// </summary>
public class TouristRoutePictureMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TouristRoutePicture, TouristRoutePictureDto>();

        config.NewConfig<TouristRoutePictureCreateDto, TouristRoutePicture>();

        config.NewConfig<TouristRoutePictureUpdateDto, TouristRoutePicture>();
    }
}