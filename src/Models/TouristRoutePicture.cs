namespace Trip.Api.Models;

/// <summary>
/// 旅游路线图片
/// </summary>
public class TouristRoutePicture
{
    public int Id { get; set; }

    public string Url { get; set; }

    public Guid TouristRouteId { get; set; }

    public TouristRoute TouristRoute { get; set; }
}