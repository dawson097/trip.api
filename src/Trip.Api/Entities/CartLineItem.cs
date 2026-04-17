using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Api.Entities;

/// <summary>
/// 购物车商品实体
/// </summary>
public class CartLineItem
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("TouristRouteId")]
    public Guid TouristRouteId { get; set; }

    public TouristRoute? TouristRoute { get; set; }

    [ForeignKey("ShoppingCartId")]
    public Guid? ShoppingCartId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OriginalPrice { get; set; }

    [Range(0.0, 1.0)]
    public double? DiscountPresent { get; set; }
}