using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Models;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRouteRepository : CommonRepository, ITouristRouteRepository
{
    private readonly TripDbContext _context;

    public TouristRouteRepository(TripDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<TouristRoute> GetAllRoutes()
    {
        return _context.TouristRoutes.Include(route => route.TouristRoutePictures).ToList();
    }

    public TouristRoute GetRouteById(Guid routeId)
    {
        return _context.TouristRoutes.Include(route => route.TouristRoutePictures)
            .FirstOrDefault(route => route.Id == routeId);
    }
}