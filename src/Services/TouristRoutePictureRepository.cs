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
        return _context.TouristRoutePictures.FirstOrDefault(picture => picture.Id == pictureId);
    }

    public void AddPicture(Guid routeId, TouristRoutePicture picture)
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
        _context.TouristRoutePictures.Add(picture);
    }
}