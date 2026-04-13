using Trip.Api.Dtos.TouristRoutePicture;

namespace Trip.Api.Dtos.TouristRoute;

/// <summary>
/// 旅游路线DTO基类
/// </summary>
public abstract class TouristRouteManipulationDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string Features { get; set; } = string.Empty;

    public string Fees { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public string TripType { get; set; } = string.Empty;

    public string TripDays { get; set; } = string.Empty;

    public string DepartureCity { get; set; } = string.Empty;

    public ICollection<TouristRoutePictureDto> TouristRoutePictures { get; set; } = new List<TouristRoutePictureDto>();
}