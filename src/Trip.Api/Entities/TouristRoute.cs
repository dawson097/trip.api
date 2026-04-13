using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Api.Entities;

/// <summary>
/// 旅游路线实体
/// </summary>
public class TouristRoute
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(1500)]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OriginalPrice { get; set; }

    [Range(0.0, 1.0)]
    public double? DiscountPresent { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    [MaxLength]
    public string Features { get; set; } = string.Empty;

    [MaxLength]
    public string Fees { get; set; } = string.Empty;

    [MaxLength]
    public string Notes { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public TripType? TripType { get; set; }

    public TripDays? TripDays { get; set; }

    public DepartureCity? DepartureCity { get; set; }

    public ICollection<TouristRoutePicture>? TouristRoutePictures { get; set; }
}