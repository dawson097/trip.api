using Trip.Api.DbContexts;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class CommonRepository : ICommonRepository
{
    private readonly TripDbContext _context;

    public CommonRepository(TripDbContext context)
    {
        _context = context;
    }

    public bool RouteExist(Guid routeId)
    {
        return _context.TouristRoutes.Any(route => route.Id == routeId);
    }
}