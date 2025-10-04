using System.Text.RegularExpressions;

namespace Trip.Api.ResourceParameters;

/// <summary>
/// 旅游路线路线资源参数
/// </summary>
public class TouristRouteResourceParameters
{
    public string Keyword { get; set; }

    public string RatingType { get; set; }

    public int? RatingValue { get; set; }

    private string _rating;

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
            }

            _rating = value;
        }
    }
}