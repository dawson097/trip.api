using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Trip.Api.Dtos.Link;
using Trip.Api.Dtos.TouristRoute;
using Trip.Api.Extensions;
using Trip.Api.Helpers;
using Trip.Api.ResourceParameters;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 旅游路线控制器路由
/// </summary>
[ApiController, Route("api/tourist-routes")]
public class TouristRoutesController(ITouristRouteService routeService, UrlHelper urlHelper)
    : ControllerBase
{
    [HttpGet(Name = "GetTouristRoutesAsync")]
    public async Task<IActionResult> GetTouristRoutesAsync([FromQuery] TouristRouteResourceParameters routeParams,
        [FromQuery] PaginationResourceParameters paginationParams, [FromHeader(Name = "Accept")] string mediaType)
    {
        if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
        {
            return BadRequest("媒体类型解析失败");
        }

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

        if (parsedMediaType.MediaType == "application/vnd.personal.hateoas+json")
        {
            var routesLinks = CreateRoutesLinks(routeParams, paginationParams);
            var routesShapedDataList = routesFromShaped.Select(route =>
            {
                var routeDict = route as IDictionary<string, object>;
                var links = CreateRouteLink((Guid)routeDict["Id"], null);
                routeDict.Add("links", links);

                return routeDict;
            });

            var routesWithLinks = new
            {
                value = routesShapedDataList,
                links = routesLinks
            };

            return Ok(routesWithLinks);
        }

        return Ok(routesFromShaped);
    }

    [HttpGet("{routeId:guid}", Name = "GetTouristRouteAsync")]
    public async Task<IActionResult> GetTouristRouteAsync([FromRoute] Guid routeId, [FromQuery] string? fields)
    {
        if (!routeService.PropertiesExists(fields))
        {
            return BadRequest("请输入正确的塑性参数");
        }

        var routeFromShaped = await routeService.GetRouteByIdAsync(routeId, fields!);

        if (routeFromShaped == null)
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var routeWithLink = routeFromShaped as IDictionary<string, object>;
        var routeLinks = CreateRouteLink(routeId, fields);
        routeWithLink.Add("links", routeLinks);

        return Ok(routeWithLink);
    }

    [HttpPost(Name = "CreateTouristRouteAsync"), Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CreateTouristRouteAsync([FromBody] TouristRouteCreateDto routeCreateDto)
    {
        var routeToReturn = await routeService.CreateRouteAsync(routeCreateDto);
        var routeLink = CreateRouteLink(routeToReturn.Id, null);
        var routeShapedData = routeToReturn.ShapeData(null) as IDictionary<string, object>;
        routeShapedData.Add("links", routeLink);

        return CreatedAtRoute("GetTouristRouteAsync", new { routeId = routeShapedData["Id"] }, routeShapedData);
    }

    [HttpPut("{routeId:guid}", Name = "UpdateTouristRouteAsync"), Authorize(AuthenticationSchemes = "Bearer")]
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

    [HttpPatch("{routeId:guid}", Name = "PartiallyUpdateTouristRouteAsync"),
     Authorize(AuthenticationSchemes = "Bearer")]
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

    [HttpDelete("{routeId:guid}", Name = "DeleteTouristRouteAsync"), Authorize(AuthenticationSchemes = "Bearer")]
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

    /// <summary>
    /// 创建旅游路线链接信息
    /// </summary>
    /// <param name="routeId">路线id</param>
    /// <param name="fields">传入字段</param>
    /// <returns>返回旅游路线链接信息</returns>
    private IEnumerable<LinkDto> CreateRouteLink(Guid routeId, string? fields)
    {
        return
        [
            // 获取旅游路线
            new LinkDto(Url.Link("GetTouristRouteAsync", new { routeId, fields })!,
                "self",
                "GET"),
            // 更新
            new LinkDto(Url.Link("UpdateTouristRouteAsync", new { routeId, fields })!,
                "update",
                "UPDATE"),
            // 局部更新
            new LinkDto(Url.Link("PartiallyUpdateTouristRouteAsync", new { routeId, fields })!,
                "partially_update",
                "PATCH"),
            // 删除
            new LinkDto(Url.Link("DeleteTouristRouteAsync", new { routeId, fields })!,
                "delete",
                "DELETE"),
            // 获取图片
            new LinkDto(Url.Link("GetTouristRoutePicturesAsync", new { routeId, fields })!,
                "get_pictures",
                "GET"),
            // 创建图片
            new LinkDto(Url.Link("CreateTouristRoutePictureAsync", new { routeId, fields })!,
                "create_pictures",
                "POST")
        ];
    }

    /// <summary>
    /// 创建旅游路线集合链接信息
    /// </summary>
    /// <param name="routeParams">路线参数</param>
    /// <param name="paginationParams">分页参数</param>
    /// <returns>返回旅游路线集合链接信息</returns>
    private IEnumerable<LinkDto> CreateRoutesLinks(TouristRouteResourceParameters routeParams,
        PaginationResourceParameters paginationParams)
    {
        return
        [
            new LinkDto(
                urlHelper.GenerateTouristRouteResourceUrl(routeParams, paginationParams, ResourceUriType.CurrentPage),
                "self",
                "GET"),
            new LinkDto(
                Url.Link("CreateTouristRouteAsync", null)!,
                "create_tourist_route",
                "GET")
        ];
    }
}