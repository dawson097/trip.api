using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/shopping-cart")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly ITouristRouteRepository _routeRepository;

    public ShoppingCartController(IShoppingCartRepository cartRepository, IHttpContextAccessor httpContext,
        IMapper mapper, ITouristRouteRepository routeRepository)
    {
        _cartRepository = cartRepository;
        _httpContext = httpContext;
        _mapper = mapper;
        _routeRepository = routeRepository;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetShoppingCart()
    {
        var userId = _httpContext.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var shoppingCart = await _cartRepository.GetShoppingCartById(userId);

        return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart!));
    }

    [HttpPost("items"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PostShoppingCartItem([FromBody] CartLineItemCreateDto itemCreateDto)
    {
        var userId = _httpContext.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var shoppingCart = await _cartRepository.GetShoppingCartById(userId);

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
        await _cartRepository.CreateShoppingCartItem(lineItem);
        await _cartRepository.SaveAsync();

        return Ok(_mapper.Map<CartLineItemDto>(lineItem));
    }
}