using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Api.Entities;

/// <summary>
/// 旅游路线图片实体
/// </summary>
public class TouristRoutePicture
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Url { get; set; } = string.Empty;

    [ForeignKey("TouristRouteId")]
    public Guid TouristRouteId { get; set; }

    public TouristRoute? TouristRoute { get; set; }
}