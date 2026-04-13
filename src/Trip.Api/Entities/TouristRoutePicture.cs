namespace Trip.Api.Entities;

public class TouristRoutePicture
{
    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public Guid TouristRouteId { get; set; }

    public TouristRoute? TouristRoute { get; set; }
}