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
        return Ok(_routeRepository.GetAllRoutes());
    }

    [HttpGet("{routeId:guid}")]
    public IActionResult GetRouteById(Guid routeId)
    {
        return Ok(_routeRepository.GetRouteById(routeId));
    }
}