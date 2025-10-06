using System.ComponentModel.DataAnnotations;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.ValidationAttributes;

namespace Trip.Api.Dtos.TouristRoute;

[RouteTitleMustBeDifferentFromDescription]
public abstract class TouristRouteManipulationDto
{
    [Required(ErrorMessage = "标题不可为空"), MaxLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "描述不可为空"), MaxLength(1500)]
    public virtual string Description { get; set; }

    public decimal Price { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string Features { get; set; }

    public string Fees { get; set; }

    public string Notes { get; set; }

    public double? Rating { get; set; }

    public string TripDays { get; set; }

    public string TripType { get; set; }

    public string DepartureCity { get; set; }

    public ICollection<TouristRoutePictureAddDto> TouristRoutePictures { get; set; } =
        new List<TouristRoutePictureAddDto>();
}