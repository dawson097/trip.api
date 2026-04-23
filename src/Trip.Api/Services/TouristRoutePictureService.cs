using MapsterMapper;
using Trip.Api.Dtos.TouristRoutePicture;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRoutePictureService(
    ICommonRepository<TouristRoutePicture> commonRepository,
    ITouristRoutePictureRepository pictureRepository,
    IMapper mapper)
    : CommonService<TouristRoutePicture>(commonRepository), ITouristRoutePictureService
{
    public async Task<IEnumerable<TouristRoutePictureDto>> GetAllPicturesByRouteIdAsync(Guid routeId)
    {
        var picturesFromRepo = await pictureRepository.GetAllPicturesByRouteIdAsync(routeId);

        return mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo);
    }

    public async Task<TouristRoutePictureDto> GetPictureByIdAsync(int pictureId)
    {
        var pictureFromRepo = await pictureRepository.GetPictureByIdAsync(pictureId);

        return mapper.Map<TouristRoutePictureDto>(pictureFromRepo);
    }

    public async Task<TouristRoutePictureDto> CreatePictureAsync(Guid routeId,
        TouristRoutePictureCreateDto pictureCreateDto)
    {
        var pictureEntity = mapper.Map<TouristRoutePicture>(pictureCreateDto);

        await pictureRepository.CreatePictureAsync(routeId, pictureEntity);
        await pictureRepository.SaveAsync();

        return mapper.Map<TouristRoutePictureDto>(pictureEntity);
    }

    public async Task UpdatePictureByIdAsync(int pictureId, TouristRoutePictureUpdateDto pictureUpdateDto)
    {
        var pictureFromRepo = await pictureRepository.GetPictureByIdAsync(pictureId);

        mapper.Map(pictureUpdateDto, pictureFromRepo);
        await pictureRepository.SaveAsync();
    }

    public async Task DeletePictureByIdAsync(int pictureId)
    {
        var pictureFromRepo = await pictureRepository.GetPictureByIdAsync(pictureId);

        pictureRepository.DeletePicture(pictureFromRepo);
        await pictureRepository.SaveAsync();
    }

    public async Task DeletePicturesByIdsAsync(IEnumerable<int> pictureIds)
    {
        var pictureItems = await pictureRepository.GetPicturesByIdsAsync(pictureIds);

        pictureRepository.DeletePictures(pictureItems);
        await pictureRepository.SaveAsync();
    }
}