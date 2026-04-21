using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Helpers;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/shopping-cart/items")]
public class CartLineItemController(
    IHttpContextAccessor httpContextAccessor,
    ICartLineItemService itemService,
    ITouristRouteService routeService)
    : ControllerBase
{
    [HttpPost, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateCartLineItem([FromBody] CartLineItemCreateDto itemCreateDto)
    {
        if (!await routeService.CheckExitsAsync(route => route.Id == itemCreateDto.TouristRouteId))
        {
            return NotFound($"旅游路线({itemCreateDto.TouristRouteId})不存在");
        }

        var userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var itemFromServ = await itemService.CreateItemAsync(userId, itemCreateDto);

        return Ok(itemFromServ);
    }

    [HttpDelete("items/{itemId:int}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteCartLineItemAsync([FromRoute] int itemId)
    {
        if (!await itemService.CheckExitsAsync(item => item.Id == itemId))
        {
            return NotFound($"商品({itemId})找不到");
        }

        await itemService.DeleteItemAsync(itemId);

        return NoContent();
    }

    [HttpDelete("items/({itemIds})"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteCartLineItemsAsync(
        [ModelBinder(BinderType = typeof(ArrayModelBinderHelper)), FromRoute]
        IEnumerable<int> itemIds)
    {
        await itemService.DeleteItemsAsync(itemIds);

        return NoContent();
    }
}