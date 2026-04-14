using System.ComponentModel.DataAnnotations;

namespace Trip.Api.Dtos.TouristRoutePicture;

/// <summary>
/// 旅游路线图片DTO基类
/// </summary>
public abstract class TouristRoutePictureManipulationDto
{
    [Required(ErrorMessage = "图片路径不应为空")]
    public string Url { get; set; } = string.Empty;
}