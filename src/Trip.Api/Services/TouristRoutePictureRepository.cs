using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRoutePictureRepository : CommonRepository, ITouristRoutePictureRepository
{
    private readonly AppDbContext _context;

    public TouristRoutePictureRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IList<TouristRoutePicture>> GetAllPicturesByRouteIdAsync(Guid routeId)
    {
        return await _context.TouristRoutePictures.Where(picture => picture.TouristRouteId == routeId).ToListAsync();
    }

    public async Task<TouristRoutePicture> GetRouteByIdAsync(int pictureId)
    {
        return (await _context.TouristRoutePictures.FirstOrDefaultAsync(picture => picture.Id == pictureId))!;
    }
}