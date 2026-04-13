using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRouteRepository : CommonRepository, ITouristRouteRepository
{
    private readonly AppDbContext _context;

    public TouristRouteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<TouristRoute>> GetAllRoutesAsync()
    {
        return await _context.TouristRoutes.ToListAsync();
    }

    public async Task<TouristRoute> GetRouteByIdAsync(Guid routeId)
    {
        return (await _context.TouristRoutes.FirstOrDefaultAsync(t => t.Id == routeId))!;
    }
}