namespace Trip.Api.Dtos.TouristRoute;

/// <summary>
/// 旅游路线特定数据DTO
/// </summary>
public class TouristRouteSimplifyDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }
}