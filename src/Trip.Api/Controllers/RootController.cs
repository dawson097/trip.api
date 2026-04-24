using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.Link;

namespace Trip.Api.Controllers;

/// <summary>
/// 根节点控制器路由
/// </summary>
/// <remarks>用于生成根文档以响应的形式放回给客户端</remarks>
[ApiController, Route("api")]
public class RootController : ControllerBase
{
    [HttpGet(Name = "GetRoot")]
    public IActionResult GetRoot()
    {
        var links = new List<LinkDto>
        {
            new(Url.Link("GetRoot", null)!,
                "self",
                "GET"),
            new(Url.Link("GetTouristRoutesAsync", null)!,
                "get_tourist_routes",
                "GET"),
            new(Url.Link("CreateTouristRouteAsync", null)!,
                "create_tourist_route",
                "POST"),
            new(Url.Link("GetShoppingCartAsync", null)!,
                "get_shopping_cart",
                "GET"),
            new(Url.Link("GetOrders", null)!,
                "get_orders",
                "GET")
        };

        return Ok(links);
    }
}