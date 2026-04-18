using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Dtos.Order;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;
using Trip.Api.Helpers;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/shopping-cart")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly ITouristRouteRepository _routeRepository;

    public ShoppingCartController(IShoppingCartRepository cartRepository, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, ITouristRouteRepository routeRepository)
    {
        _cartRepository = cartRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _routeRepository = routeRepository;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetShoppingCartsAsync()
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var shoppingCart = await _cartRepository.GetShoppingCartByIdAsync(userId);

        return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart!));
    }

    [HttpPost("items"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PostShoppingCartItemAsync([FromBody] CartLineItemCreateDto itemCreateDto)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var shoppingCart = await _cartRepository.GetShoppingCartByIdAsync(userId);

        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(itemCreateDto.TouristRouteId);

        if (routeFromRepo == null)
        {
            return NotFound($"旅游路线({itemCreateDto.TouristRouteId})不存在");
        }

        var lineItem = new CartLineItem
        {
            TouristRouteId = itemCreateDto.TouristRouteId,
            ShoppingCartId = shoppingCart!.Id,
            OriginalPrice = routeFromRepo.OriginalPrice,
            DiscountPresent = routeFromRepo.DiscountPresent
        };
        await _cartRepository.AddShoppingCartItemAsync(lineItem);
        await _cartRepository.SaveAsync();

        return Ok(_mapper.Map<CartLineItemDto>(lineItem));
    }

    [HttpPost("checkout"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CheckoutAsync()
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var shoppingCart = await _cartRepository.GetShoppingCartByIdAsync(userId);
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderState = OrderState.Pending,
            OrderItems = shoppingCart.CartLineItems,
            CreateTimeUtc = DateTime.UtcNow
        };

        shoppingCart.CartLineItems = null;

        await _cartRepository.AddOrderAsync(order);
        await _cartRepository.SaveAsync();

        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpDelete("items/{itemId:int}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteShoppingCartItemAsync([FromRoute] int itemId)
    {
        var lineItem = await _cartRepository.GetCartLineItemByIdAsync(itemId);

        if (lineItem == null)
        {
            return NotFound($"商品({itemId})找不到");
        }

        _cartRepository.DeleteShoppingCartItem(lineItem);
        await _cartRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("items/({itemIds})"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteShoppingCartItemsAsync(
        [ModelBinder(BinderType = typeof(ArrayModelBinderHelper)), FromRoute]
        IEnumerable<int> itemIds)
    {
        var lineItems = await _cartRepository.GetCartLineItemsByItemIdsAsync(itemIds);

        _cartRepository.DeleteShoppingCartItems(lineItems);
        await _cartRepository.SaveAsync();

        return NoContent();
    }
}