using Trip.Api.ResourceParameters;

namespace Trip.Api.Helpers;

/// <summary>
/// URL工具
/// </summary>
public class UrlHelper(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
{
    /// <summary>
    /// 生成旅游路线资源URL
    /// </summary>
    /// <param name="routeParameters">路线参数</param>
    /// <param name="paginationParams">分页参数</param>
    /// <param name="uriType"></param>
    /// <returns>返回完成生成的旅游路线资源URL</returns>
    public string GenerateTouristRouteResourceUrl(TouristRouteResourceParameters routeParameters,
        PaginationResourceParameters paginationParams, ResourceUriType uriType)
    {
        return (uriType switch
        {
            ResourceUriType.PreviousPage => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!,
                "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber,
                    orderBy = routeParameters.OrderBy
                }),
            ResourceUriType.NextPage => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!,
                "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber + 1,
                    orderBy = routeParameters.OrderBy
                }),
            _ => linkGenerator.GetUriByRouteValues(httpContextAccessor.HttpContext!, "GetTouristRoutesAsync",
                new
                {
                    keyword = routeParameters.Keyword,
                    ratingType = routeParameters.RatingType,
                    pageSize = paginationParams.PageSize,
                    pageNumber = paginationParams.PageNumber,
                    orderBy = routeParameters.OrderBy
                })
        })!;
    }
}