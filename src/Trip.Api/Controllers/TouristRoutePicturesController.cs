using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 旅游路线图片控制器路由
/// </summary>
[ApiController, Route("api/tourist-routes/{routeId:guid}/pictures")]
public class TouristRoutePicturesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITouristRoutePictureRepository _pictureRepository;

    public TouristRoutePicturesController(IMapper mapper, ITouristRoutePictureRepository pictureRepository)
    {
        _mapper = mapper;
        _pictureRepository = pictureRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTouristRoutePicturesAsync([FromRoute] Guid routeId)
    {
        if (!await _pictureRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var picturesFromRepo = await _pictureRepository.GetAllPicturesByRouteIdAsync(routeId);

        if (picturesFromRepo == null || !picturesFromRepo.Any())
        {
            return NotFound("找不到任何图片");
        }

        return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
    }

    [HttpGet("{pictureId:int}", Name = "GetTouristRoutePictureAsync")]
    public async Task<IActionResult> GetTouristRoutePictureAsync([FromRoute] Guid routeId, [FromRoute] int pictureId)
    {
        if (!await _pictureRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var pictureFromRepo = await _pictureRepository.GetPictureByIdAsync(pictureId);

        if (pictureFromRepo == null)
        {
            return NotFound($"图片({pictureId})找不到");
        }

        return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
    }

    [HttpPost]
    public async Task<IActionResult> PostTouristRoutePictureAsync([FromRoute] Guid routeId,
        [FromBody] TouristRoutePictureCreateDto pictureCreateDto)
    {
        if (!await _pictureRepository.RoutesExitsAsync(routeId))
        {
            return NotFound($"旅游路线({routeId})不存在");
        }

        var pictureEntity = _mapper.Map<TouristRoutePicture>(pictureCreateDto);

        await _pictureRepository.AddPictureAsync(routeId, pictureEntity);
        await _pictureRepository.SaveAsync();

        var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureEntity);

        return CreatedAtRoute("GetTouristRoutePictureAsync", new
        {
            routeId = pictureToReturn.TouristRouteId,
            pictureId = pictureToReturn.Id
        }, pictureToReturn);
    }

    [HttpPut("{pictureId:int}")]
    public async Task<IActionResult> PutTouristRoutePictureAsync([FromRoute] int pictureId,
        [FromBody] TouristRoutePictureUpdateDto pictureUpdateDto)
    {
        if (!await _pictureRepository.PicturesExitsAsync(pictureId))
        {
            return NotFound($"图片({pictureId})不存在");
        }

        var pictureFromRepo = await _pictureRepository.GetPictureByIdAsync(pictureId);

        _mapper.Map(pictureUpdateDto, pictureFromRepo);
        await _pictureRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{pictureId:int}")]
    public async Task<IActionResult> DeleteTouristRoutePictureAsync([FromRoute] int pictureId)
    {
        if (!await _pictureRepository.PicturesExitsAsync(pictureId))
        {
            return NotFound($"图片({pictureId})不存在");
        }

        var pictureFromRepo = await _pictureRepository.GetPictureByIdAsync(pictureId);

        _pictureRepository.DeletePicture(pictureFromRepo);
        await _pictureRepository.SaveAsync();

        return NoContent();
    }
}