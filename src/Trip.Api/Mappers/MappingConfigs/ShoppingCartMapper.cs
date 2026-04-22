using Mapster;
using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;

namespace Trip.Api.Mappers.MappingConfigs;

public class ShoppingCartMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCart, ShoppingCartDto>();

        config.NewConfig<CartLineItem, CartLineItemDto>();
    }
}