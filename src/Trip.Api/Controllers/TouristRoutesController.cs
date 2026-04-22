using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Helpers;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 旅游路线控制器路由
/// </summary>
[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController(ITouristRouteService routeService)
    : ControllerBase
{
    [HttpGet(Name = "GetTouristRoutesAsync")]
    public async Task<IActionResult> GetTouristRoutesAsync([FromQuery] TouristRouteResourceParameters routeParams,
        [FromQuery] PaginationResourceParameters paginationParams)
    {
        if (!routeService.MappingExists(routeParams.OrderBy!))
        {
            return BadRequest("请输入正确的排序参数");
        }

        if (!routeService.PropertiesExists(routeParams.Fields!))
        {
            return BadRequest("请输入正确的塑性参数");
        }

        var (routesFromShaped, paginationMetaData) =
            await routeService.GetAllRoutesAsync(routeParams, paginationParams);

        if (routesFromShaped == null || !routesFromShaped.Any())
        {
            return NotFound("找不到任何旅游路线");
        }

        Response.Headers.Append("x-pagination", JsonConvert.SerializeObject(paginationMetaData));

        return Ok(routesFromShaped);
    }

    [HttpGet("{routeId:guid}", Name = "GetTouristRouteAsync")]
    public async Task<IActionResult> GetTouristRouteAsync([FromRoute] Guid routeId, [FromQuery] string fields)
    {
        if (!routeService.PropertiesExists(fields))
        {
            return BadRequest("请输入正确的塑性参数");
        }

        var routeFromShaped = await routeService.GetRouteByIdAsync(routeId, fields);

        if (routeFromShaped == null)
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        return Ok(routeFromShaped);
    }

    [HttpPost, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateTouristRouteAsync([FromBody] TouristRouteCreateDto routeCreateDto)
    {
        var routeToReturn = await routeService.CreateRouteAsync(routeCreateDto);

        return CreatedAtRoute("GetTouristRouteAsync", new { routeId = routeToReturn.Id }, routeToReturn);
    }

    [HttpPut("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateTouristRouteAsync([FromRoute] Guid routeId,
        [FromBody] TouristRouteUpdateDto routeUpdateDto)
    {
        if (!await routeService.CheckExitsAsync(route => route.Id == routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        await routeService.UpdateRouteAsync(routeId, routeUpdateDto);

        return NoContent();
    }

    [HttpPatch("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> PartiallyUpdateTouristRouteAsync([FromRoute] Guid routeId,
        [FromBody] JsonPatchDocument<TouristRouteUpdateDto> patchDoc)
    {
        if (!await routeService.CheckExitsAsync(route => route.Id == routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var routeFromServ = await routeService.GetPartialUpdateRouteByIdAsync(routeId);

        patchDoc.ApplyTo(routeFromServ, ModelState);

        if (!TryValidateModel(routeFromServ))
        {
            return ValidationProblem(ModelState);
        }

        await routeService.PartialUpdateRouteAsync(routeId);

        return NoContent();
    }

    [HttpDelete("{routeId:guid}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRouteAsync([FromRoute] Guid routeId)
    {
        if (!await routeService.CheckExitsAsync(route => route.Id == routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        await routeService.DeleteRouteAsync(routeId);

        return NoContent();
    }

    [HttpDelete("({routeIds}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRoutesAsync(
        [ModelBinder(BinderType = typeof(ArrayModelBinderHelper)), FromRoute]
        IEnumerable<Guid> routeIds)
    {
        await routeService.DeleteRoutesAsync(routeIds);

        return NoContent();
    }
}