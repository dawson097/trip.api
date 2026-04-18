using Trip.Api.Dtos.CartLineItem;

namespace Trip.Api.Dtos.Order;

/// <summary>
/// 订单DTO
/// </summary>
public class OrderDto
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ICollection<CartLineItemDto>? OrderItems { get; set; }

    public string OrderState { get; set; } = string.Empty;

    public DateTime CreateDateUtc { get; set; }

    public string TransactionMetadata { get; set; } = string.Empty;
}