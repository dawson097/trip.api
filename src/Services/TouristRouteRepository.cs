using Trip.Api.DbContexts;
using Trip.Api.Models;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRouteRepository : ITouristRouteRepository
{
    private readonly TripDbContext _context;

    public TouristRouteRepository(TripDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TouristRoute> GetAllRoutes()
    {
        return _context.TouristRoutes.ToList();
    }

    public TouristRoute GetRouteById(Guid routeId)
    {
        return _context.TouristRoutes.FirstOrDefault(route => route.Id == routeId);
    }
}