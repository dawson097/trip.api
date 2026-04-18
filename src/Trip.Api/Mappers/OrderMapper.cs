using Mapster;
using Trip.Api.Dtos.Order;
using Trip.Api.Entities;

namespace Trip.Api.Mappers;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.OrderState, src => src.OrderState.ToString());
    }
}