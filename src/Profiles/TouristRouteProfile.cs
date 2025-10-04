using AutoMapper;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Models;

namespace Trip.Api.Profiles;

/// <summary>
/// 旅游路线映射配置
/// </summary>
public class TouristRouteProfile : Profile
{
    public TouristRouteProfile()
    {
        CreateMap<TouristRoute, TouristRouteDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                src.OriginalPrice * (decimal)(src.DiscountPresent ?? 1)
            ))
            .ForMember(dest => dest.TripDays, opt => opt.MapFrom(src =>
                src.TripDays.ToString()
            ))
            .ForMember(dest => dest.TripType, opt => opt.MapFrom(src =>
                src.TripType.ToString()
            ))
            .ForMember(dest => dest.DepartureCity, opt => opt.MapFrom(src =>
                src.DepartureCity.ToString()
            ));
    }
}