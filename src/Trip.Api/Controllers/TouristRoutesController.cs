using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Entities;
using Trip.Api.Helpers;
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
    public async Task<IActionResult> GetTouristRoutesAsync([FromQuery] TouristRouteResourceParameter routeParams,
        [FromQuery] PaginationResourceParameter paginationParams)
    {
        var routesFromRepo =
            await _routeRepository.GetAllRoutesAsync(routeParams.Keyword, routeParams.RatingType,
                routeParams.RatingValue, paginationParams.PageSize, paginationParams.PageNumber);

        if (routesFromRepo == null || !routesFromRepo.Any())
        {
            return NotFound("找不到任何旅游路线");
        }

        return Ok(_mapper.Map<List<TouristRouteDto>>(routesFromRepo));
    }

    [HttpGet("{routeId:guid}", Name = "GetTouristRouteAsync")]
    public async Task<IActionResult> GetTouristRouteAsync([FromRoute] Guid routeId)
    {
        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(routeId);

        if (routeFromRepo == null)
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        return Ok(_mapper.Map<TouristRouteDto>(routeFromRepo));
    }

    [HttpPost, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PostTouristRouteAsync([FromBody] TouristRouteCreateDto routeCreateDto)
    {
        var routeEntity = _mapper.Map<TouristRoute>(routeCreateDto);

        await _routeRepository.AddRouteAsync(routeEntity);
        await _routeRepository.SaveAsync();

        var routeToReturn = _mapper.Map<TouristRouteDto>(routeEntity);

        return CreatedAtRoute("GetTouristRouteAsync", new { routeId = routeToReturn.Id }, routeToReturn);
    }

    [HttpPut("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PutTouristRouteAsync([FromRoute] Guid routeId,
        [FromBody] TouristRouteUpdateDto routeUpdateDto)
    {
        if (!await _routeRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(routeId);

        _mapper.Map(routeUpdateDto, routeFromRepo);
        await _routeRepository.SaveAsync();

        return NoContent();
    }

    [HttpPatch("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PatchTouristRouteAsync([FromRoute] Guid routeId,
        [FromBody] JsonPatchDocument<TouristRouteUpdateDto> patchDoc)
    {
        if (!await _routeRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(routeId);
        var routeToPatch = _mapper.Map<TouristRouteUpdateDto>(routeFromRepo);

        patchDoc.ApplyTo(routeToPatch, ModelState);

        if (!TryValidateModel(routeToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(routeToPatch, routeFromRepo);
        await _routeRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRouteAsync([FromRoute] Guid routeId)
    {
        if (!await _routeRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var routeFromRepo = await _routeRepository.GetRouteByIdAsync(routeId);

        _routeRepository.DeleteRoute(routeFromRepo);
        await _routeRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("({routeIds}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRoutesAsync(
        [ModelBinder(BinderType = typeof(ArrayModelBinderHelper)), FromRoute]
        IEnumerable<Guid> routeIds)
    {
        var routeItems = await _routeRepository.GetRoutesByIdsAsync(routeIds);

        _routeRepository.DeleteRoutes(routeItems);
        await _routeRepository.SaveAsync();

        return NoContent();
    }
}