using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Api.Entities;

/// <summary>
/// 订单实体
/// </summary>
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("AppUser")]
    public string? UserId { get; set; }

    public AppUser? AppUser { get; set; }

    public ICollection<CartLineItem>? OrderItems { get; set; }

    public OrderState OrderState { get; set; }

    public DateTime CreateTimeUtc { get; set; }

    public string? TransactionMetadata { get; set; }
}