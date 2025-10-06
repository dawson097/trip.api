using System.ComponentModel.DataAnnotations;
using Trip.Api.Dtos.TouristRoute;

namespace Trip.Api.ValidationAttributes;

public class RouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var routeDto = (TouristRouteManipulationDto)validationContext.ObjectInstance;

        if (routeDto.Title == routeDto.Description)
        {
            return new ValidationResult("标题与描述必须不一致", new[] { "TouristRouteAddDto" });
        }

        return ValidationResult.Success;
    }
}