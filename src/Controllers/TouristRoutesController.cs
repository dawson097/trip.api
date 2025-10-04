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
    public IActionResult GetAllRoutes([FromQuery] string keyword, string rating)
    {
        var regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
        string ratingType = "";
        int ratingValue = -1;

        var match = regex.Match(rating);

        if (match.Success)
        {
            ratingType = match.Groups[1].Value;
            ratingValue = int.Parse(match.Groups[2].Value);
        }

        var routesFromRepo = _routeRepository.GetAllRoutes(keyword, ratingType, ratingValue);

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