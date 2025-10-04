namespace Trip.Api.Models;

/// <summary>
/// 旅游路线
/// </summary>
public class TouristRoute
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal OriginalPrice { get; set; }

    public double? DiscountPresent { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string Features { get; set; }

    public string Fees { get; set; }

    public string Notes { get; set; }

    public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; } = new List<TouristRoutePicture>();
}