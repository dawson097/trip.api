using AutoMapper;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Models;

namespace Trip.Api.Profiles;

/// <summary>
/// 旅游路线图片映射配置
/// </summary>
public class TouristRoutePictureProfile : Profile
{
    public TouristRoutePictureProfile()
    {
        CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
    }
}