using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 旅游路线图片控制器路由
/// </summary>
[ApiController, Route("api/tourist-routes/{routeId:guid}/pictures")]
public class TouristRoutePicturesController(ITouristRoutePictureService pictureService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTouristRoutePicturesAsync([FromRoute] Guid routeId)
    {
        if (!await pictureService.CheckExitsAsync(route => route.TouristRouteId == routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var picturesFromServ = await pictureService.GetAllPicturesByRouteIdAsync(routeId);

        if (picturesFromServ == null || !picturesFromServ.Any())
        {
            return NotFound("找不到任何图片");
        }

        return Ok(picturesFromServ);
    }

    [HttpGet("{pictureId:int}", Name = "GetTouristRoutePictureAsync")]
    public async Task<IActionResult> GetTouristRoutePictureAsync([FromRoute] Guid routeId, [FromRoute] int pictureId)
    {
        if (!await pictureService.CheckExitsAsync(route => route.TouristRouteId == routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var pictureFromServ = await pictureService.GetPictureByIdAsync(pictureId);

        if (pictureFromServ == null)
        {
            return NotFound($"图片({pictureId})找不到");
        }

        return Ok(pictureFromServ);
    }

    [HttpPost, Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateTouristRoutePictureAsync([FromRoute] Guid routeId,
        [FromBody] TouristRoutePictureCreateDto pictureCreateDto)
    {
        if (!await pictureService.CheckExitsAsync(route => route.TouristRouteId == routeId))
        {
            return NotFound($"旅游路线({routeId})不存在");
        }

        var pictureToReturn = await pictureService.CreateTouristRoutePictureAsync(routeId, pictureCreateDto);

        return CreatedAtRoute("GetTouristRoutePictureAsync", new
        {
            routeId = pictureToReturn.TouristRouteId,
            pictureId = pictureToReturn.Id
        }, pictureToReturn);
    }

    [HttpPut("{pictureId:int}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateTouristRoutePictureAsync([FromRoute] Guid routeId, [FromRoute] int pictureId,
        [FromBody] TouristRoutePictureUpdateDto pictureUpdateDto)
    {
        if (!await pictureService.CheckExitsAsync(route => route.TouristRouteId == routeId))
        {
            return NotFound($"旅游路线({routeId})不存在");
        }

        if (!await pictureService.CheckExitsAsync(picture => picture.Id == pictureId))
        {
            return NotFound($"图片({pictureId})不存在");
        }

        await pictureService.UpdatePictureByIdAsync(pictureId, pictureUpdateDto);

        return NoContent();
    }

    [HttpDelete("{pictureId:int}"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRoutePictureAsync([FromRoute] Guid routeId, [FromRoute] int pictureId)
    {
        if (!await pictureService.CheckExitsAsync(route => route.TouristRouteId == routeId))
        {
            return NotFound($"旅游路线({routeId})不存在");
        }

        if (!await pictureService.CheckExitsAsync(picture => picture.Id == pictureId))
        {
            return NotFound($"图片({pictureId})不存在");
        }

        await pictureService.DeletePictureByIdAsync(pictureId);

        return NoContent();
    }

    [HttpDelete("({pictureIds})"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteTouristRoutePicturesAsync([FromRoute] IEnumerable<int> pictureIds)
    {
        await pictureService.DeletePicturesByIdsAsync(pictureIds);

        return NoContent();
    }
}