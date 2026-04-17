using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Api.Entities;

/// <summary>
/// 购物车实体
/// </summary>
public class ShoppingCart
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("AppUser")]
    public string? UserId { get; set; }

    public AppUser? AppUser { get; set; }

    public ICollection<CartLineItem>? CartLineItems { get; set; }
}