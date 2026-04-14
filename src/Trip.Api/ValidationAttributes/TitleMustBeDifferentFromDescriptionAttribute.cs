using System.ComponentModel.DataAnnotations;
using Trip.Api.Dtos.TouristRoute;

namespace Trip.Api.ValidationAttributes;

/// <summary>
/// 标题必须与描述不一致
/// </summary>
public class TitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var routeDto = (TouristRouteManipulationDto)validationContext.ObjectInstance;

        if (routeDto.Title == routeDto.Description)
        {
            return new ValidationResult("标题必须和描述不一致", ["TouristRouteManipulationDto"]);
        }

        return ValidationResult.Success;
    }
}