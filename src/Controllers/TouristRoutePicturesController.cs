using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

[ApiController, Route("api/tourist-routes/{routeId:guid}/pictures")]
public class TouristRoutePicturesController : ControllerBase
{
    private readonly ITouristRoutePictureRepository _pictureRepository;
    private readonly IMapper _mapper;

    public TouristRoutePicturesController(ITouristRoutePictureRepository pictureRepository, IMapper mapper)
    {
        _pictureRepository = pictureRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllRoutes(Guid routeId)
    {
        if (!_pictureRepository.RouteExist(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var picturesFromRepo = _pictureRepository.GetAllPicturesByRouteId(routeId);

        if (picturesFromRepo == null || !picturesFromRepo.Any())
        {
            return NotFound($"旅游路线({routeId})下找不到任何旅游路线图片");
        }

        return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
    }

    [HttpGet("{pictureId:int}")]
    public IActionResult GetPictureById(Guid routeId, int pictureId)
    {
        if (!_pictureRepository.RouteExist(routeId))
        {
            return NotFound($"旅游路线({routeId})找不到");
        }

        var pictureFromRepo = _pictureRepository.GetPictureById(pictureId);

        if (pictureFromRepo == null)
        {
            return NotFound($"图片id({pictureId})对应图片找不到");
        }

        return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
    }
}