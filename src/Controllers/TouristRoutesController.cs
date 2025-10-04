using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController : ControllerBase
{
    private readonly ITouristRouteRepository _routeRepository;
    private readonly IMapper _mapper;

    public TouristRoutesController(ITouristRouteRepository routeRepository, IMapper mapper)
    {
        _routeRepository = routeRepository;
        _mapper = mapper;
    }

    [HttpGet, HttpHead]
    public IActionResult GetAllRoutes([FromQuery] string keyword)
    {
        var routesFromRepo = _routeRepository.GetAllRoutes(keyword);

        if (routesFromRepo == null || !routesFromRepo.Any())
        {
            return NotFound("找不到任何旅游路线");
        }

        return Ok(_mapper.Map<IEnumerable<TouristRouteDto>>(routesFromRepo));
    }

    [HttpGet("{routeId:guid}")]
    public IActionResult GetRouteById(Guid routeId)
    {
        var routeFromRepo = _routeRepository.GetRouteById(routeId);

        if (routeFromRepo == null)
        {
            return NotFound($"路线id为({routeId})的旅游路线找不到");
        }

        return Ok(_mapper.Map<TouristRouteDto>(routeFromRepo));
    }
}