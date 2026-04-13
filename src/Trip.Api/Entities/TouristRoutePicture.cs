namespace Trip.Api.Entities;

/// <summary>
/// 旅游路线图片实体
/// </summary>
public class TouristRoutePicture
{
    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public Guid TouristRouteId { get; set; }

    public TouristRoute? TouristRoute { get; set; }
}