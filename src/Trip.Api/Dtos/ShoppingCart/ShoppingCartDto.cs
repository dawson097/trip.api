using Trip.Api.Dtos.CartLineItem;

namespace Trip.Api.Dtos.ShoppingCart;

/// <summary>
/// 购物车DTO
/// </summary>
public class ShoppingCartDto
{
    public Guid Id { get; set; }

    public string? UserId { get; set; }

    public ICollection<CartLineItemDto>? CartLineItems { get; set; }
}