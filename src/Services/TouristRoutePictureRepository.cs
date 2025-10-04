using Trip.Api.DbContexts;
using Trip.Api.Models;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRoutePictureRepository : CommonRepository, ITouristRoutePictureRepository
{
    private readonly TripDbContext _context;

    public TouristRoutePictureRepository(TripDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<TouristRoutePicture> GetAllPicturesByRouteId(Guid routeId)
    {
        return _context.TouristRoutePictures.Where(picture => picture.TouristRouteId == routeId).ToList();
    }

    public TouristRoutePicture GetPictureById(int pictureId)
    {
        return _context.TouristRoutePictures.Where(picture => picture.Id == pictureId).FirstOrDefault();
    }
}