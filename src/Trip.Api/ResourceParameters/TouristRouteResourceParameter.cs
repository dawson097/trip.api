using System.Text.RegularExpressions;

namespace Trip.Api.ResourceParameters;

/// <summary>
/// 旅游路线数据过滤
/// </summary>
public class TouristRouteResourceParameter
{
    private string _rating = string.Empty;

    public string Keyword { get; set; } = string.Empty;

    public string RatingType { get; set; } = string.Empty;

    public int? RatingValue { get; set; }

    public string Rating
    {
        get => _rating;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                var match = regex.Match(value);

                if (match.Success)
                {
                    RatingType = match.Groups[1].Value;
                    RatingValue = int.Parse(match.Groups[2].Value);
                }

                _rating = value;
            }
        }
    }
}