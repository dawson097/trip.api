using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 旅游路线控制器路由
/// </summary>
[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITouristRouteRepository _routeRepository;

    public TouristRoutesController(IMapper mapper, ITouristRouteRepository routeRepository)
    {
        _mapper = mapper;
        _routeRepository = routeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTouristRoutesAsync([FromQuery] TouristRouteResourceParameter routeParams)
    {
        var routesFromRepo =
            await _routeRepository.GetAllRoutesAsync(routeParams.Keyword, routeParams.RatingType,
                routeParams.RatingValue);

        if (routesFromRepo == null || !routesFromRepo.Any())
        {
            return NotFound("找不到任何旅游路线");
        }

        return Ok(_mapper.Map<List<TouristRouteDto>>(routesFromRepo));
    }

    [HttpGet("{routeId:guid}")]
    public async Task<IActionResult> GetTouristRouteAsync([FromRoute] Guid routeId)
    {
        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(routeId);

        if (routeFromRepo == null)
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        return Ok(_mapper.Map<TouristRouteDto>(routeFromRepo));
    }
}