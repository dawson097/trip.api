using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;

namespace Trip.Api.Repositories;

public class TouristRoutePictureRepository(AppDbContext context)
    : CommonRepository<TouristRoutePicture>(context), ITouristRoutePictureRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IList<TouristRoutePicture>> GetAllPicturesByRouteIdAsync(Guid routeId)
    {
        return await _context.TouristRoutePictures.Where(picture => picture.TouristRouteId == routeId).ToListAsync();
    }

    public async Task<TouristRoutePicture> GetPictureByIdAsync(int pictureId)
    {
        return (await _context.TouristRoutePictures.FirstOrDefaultAsync(picture => picture.Id == pictureId))!;
    }

    public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByIdsAsync(IEnumerable<int> pictureIds)
    {
        return await _context.TouristRoutePictures.Where(picture => pictureIds.Contains(picture.Id)).ToListAsync();
    }

    public async Task CreatePictureAsync(Guid routeId, TouristRoutePicture picture)
    {
        if (routeId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(routeId));
        }

        if (picture == null)
        {
            throw new ArgumentNullException(nameof(picture));
        }

        picture.TouristRouteId = routeId;
        await _context.TouristRoutePictures.AddAsync(picture);
    }

    public void DeletePicture(TouristRoutePicture picture)
    {
        _context.TouristRoutePictures.Remove(picture);
    }

    public void DeletePictures(IEnumerable<TouristRoutePicture> pictures)
    {
        _context.TouristRoutePictures.RemoveRange(pictures);
    }
}