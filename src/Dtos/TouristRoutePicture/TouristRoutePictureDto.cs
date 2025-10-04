namespace Trip.Api.Dtos.TouristRoutePicture;

/// <summary>
/// 旅游路线图片DTO
/// </summary>
public class TouristRoutePictureDto
{
    public int Id { get; set; }

    public string Url { get; set; }

    public Guid TouristRouteId { get; set; }
}