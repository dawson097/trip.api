using Mapster;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Entities;

namespace Trip.Api.Mappers;

/// <summary>
/// 旅游路线映射
/// </summary>
public class TouristRouteMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TouristRoute, TouristRouteDto>()
            .Map(dest => dest.Price, src => src.OriginalPrice * (decimal)(src.DiscountPresent ?? 1))
            .Map(dest => dest.TripType, src => src.TripType.ToString())
            .Map(dest => dest.TripDays, src => src.TripDays.ToString())
            .Map(dest => dest.DepartureCity, src => src.DepartureCity.ToString());

        config.NewConfig<TouristRouteCreateDto, TouristRoute>()
            .Map(dest => dest.Id, src => Guid.NewGuid());

        config.NewConfig<TouristRouteUpdateDto, TouristRoute>();
    }
}