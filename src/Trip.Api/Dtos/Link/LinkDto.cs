namespace Trip.Api.Dtos.Link;

/// <summary>
/// 链接DTO
/// </summary>
public class LinkDto(string href, string rel, string method)
{
    // URL
    public string Href { get; set; } = href;

    // 关系
    public string Rel { get; set; } = rel;

    // 方法
    public string Method { get; set; } = method;
}