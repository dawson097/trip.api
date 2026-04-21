using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/shopping-cart")]
public class ShoppingCartController(
    IHttpContextAccessor httpContextAccessor,
    IShoppingCartService cartService)
    : ControllerBase
{
    [HttpGet, Authorize]
    public async Task<IActionResult> GetShoppingCartsAsync()
    {
        var userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var routeFromServ = await cartService.GetShoppingCartByUserIdAsync(userId);

        return Ok(routeFromServ);
    }

    [HttpPost("checkout"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CheckoutAsync()
    {
        var userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cartFromServ = await cartService.CheckoutAsync(userId);

        return Ok(cartFromServ);
    }
}