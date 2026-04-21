using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 订单控制器路由
/// </summary>
[ApiController, Route("api/orders")]
public class OrdersController(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IOrderService orderService)
    : ControllerBase
{
    [HttpGet, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] PaginationResourceParameter paginationParams)
    {
        var userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var orderFromServ = await orderService.GetAllOrdersAsync(userId, paginationParams);

        return Ok(orderFromServ);
    }


    [HttpGet("{orderId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrderAsync(Guid orderId)
    {
        var orderFromServ = await orderService.GetOrderAsync(orderId);

        return Ok(orderFromServ);
    }

    [HttpPost("{orderId:guid}/place-order"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PlaceOrderAsync([FromRoute] Guid orderId)
    {
        var orderFromServ = await orderService.PlaceOrderAsync(orderId);

        return Ok(orderFromServ);
    }
}