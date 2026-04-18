using System.Security.Claims;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.Order;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 订单控制器路由
/// </summary>
[ApiController, Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IHttpContextAccessor httpContextAccessor, IMapper mapper, IOrderRepository orderRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    [HttpGet, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var orders = await _orderRepository.GetAllOrdersAsync(userId);

        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }


    [HttpGet("{orderId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrderAsync(Guid orderId)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        return Ok(_mapper.Map<OrderDto>(order));
    }
}