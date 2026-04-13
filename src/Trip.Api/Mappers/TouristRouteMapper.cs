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
        config.NewConfig<TouristRoute, TouristRouteDto>();
    }
}