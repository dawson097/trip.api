using Microsoft.AspNetCore.Mvc;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController : ControllerBase
{
    private readonly ITouristRouteRepository _routeRepository;

    public TouristRoutesController(ITouristRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    [HttpGet]
    public IActionResult GetAllRoutes()
    {
        var routesFromRepo = _routeRepository.GetAllRoutes();

        if (routesFromRepo == null || !routesFromRepo.Any())
        {
            return NotFound("找不到任何旅游路线");
        }

        return Ok(routesFromRepo);
    }

    [HttpGet("{routeId:guid}")]
    public IActionResult GetRouteById(Guid routeId)
    {
        var routeFromRepo = _routeRepository.GetRouteById(routeId);

        if (routeFromRepo == null)
        {
            return NotFound($"路线id为({routeId})的旅游路线找不到");
        }

        return Ok(routeFromRepo);
    }
}