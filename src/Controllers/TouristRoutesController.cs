using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Models;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITouristRouteRepository _routeRepository;

    public TouristRoutesController(ITouristRouteRepository routeRepository, IMapper mapper)
    {
        _routeRepository = routeRepository;
        _mapper = mapper;
    }

    [HttpGet, HttpHead]
    public IActionResult GetAllRoutes([FromQuery] TouristRouteResourceParameters routeParams)
    {
        var routesFromRepo = _routeRepository.GetAllRoutes(routeParams.Keyword,
            routeParams.RatingType,
            routeParams.RatingValue);

        if (routesFromRepo == null || !routesFromRepo.Any())
            return NotFound("找不到任何旅游路线");

        return Ok(_mapper.Map<IEnumerable<TouristRouteDto>>(routesFromRepo));
    }

    [HttpGet("{routeId:guid}", Name = "GetRouteById")]
    public IActionResult GetRouteById(Guid routeId)
    {
        var routeFromRepo = _routeRepository.GetRouteById(routeId);

        if (routeFromRepo == null)
            return NotFound($"路线id为({routeId})的旅游路线找不到");

        return Ok(_mapper.Map<TouristRouteDto>(routeFromRepo));
    }

    [HttpPost]
    public IActionResult AddRoute([FromBody] TouristRouteAddDto routeAddDto)
    {
        var routeModel = _mapper.Map<TouristRoute>(routeAddDto);

        _routeRepository.AddRoute(routeModel);
        _routeRepository.Save();

        var routeToReturn = _mapper.Map<TouristRouteDto>(routeModel);

        return CreatedAtRoute("GetRouteById", new { routeId = routeToReturn.Id }, routeToReturn);
    }
}