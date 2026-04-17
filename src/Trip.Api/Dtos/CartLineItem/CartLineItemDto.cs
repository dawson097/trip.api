using Trip.Api.Dtos.TouristRoute;

namespace Trip.Api.Dtos.CartLineItem;

/// <summary>
/// 购物车商品列表DTO
/// </summary>
public class CartLineItemDto
{
    public int Id { get; set; }

    public Guid TouristRouteId { get; set; }

    public TouristRouteDto? TouristRoute { get; set; }

    public Guid? ShoppingCartId { get; set; }

    public decimal OriginalPrice { get; set; }

    public double? DiscountPresent { get; set; }
}