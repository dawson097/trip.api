namespace Trip.Api.Entities;

/// <summary>
/// 旅游路线实体
/// </summary>
public class TouristRoute
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal OriginalPrice { get; set; }

    public double? DiscountPresent { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string Features { get; set; } = string.Empty;

    public string Fees { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public TripType? TripType { get; set; }

    public TripDays? TripDays { get; set; }

    public DepartureCity? DepartureCity { get; set; }

    public ICollection<TouristRoutePicture>? TouristRoutePictures { get; set; }
}