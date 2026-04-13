using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class TouristRouteRepository : CommonRepository, ITouristRouteRepository
{
    private readonly AppDbContext _context;

    public TouristRouteRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IList<TouristRoute>> GetAllRoutesAsync(string keyword, string ratingType, int? ratingValue)
    {
        IQueryable<TouristRoute> queryRes = _context.TouristRoutes.Include(route => route.TouristRoutePictures);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            queryRes = queryRes.Where(route => route.Title.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(ratingType))
        {
            queryRes = ratingType switch
            {
                "largerThan" => queryRes.Where(route => route.Rating >= ratingValue),
                "lessThan" => queryRes.Where(route => route.Rating <= ratingValue),
                _ => queryRes.Where(route => route.Rating == ratingValue)
            };
        }

        return await queryRes.ToListAsync();
    }

    public async Task<TouristRoute> GetRouteByIdAsync(Guid routeId)
    {
        return (await _context.TouristRoutes.Include(route => route.TouristRoutePictures)
            .FirstOrDefaultAsync(t => t.Id == routeId))!;
    }

    public async Task AddRouteAsync(TouristRoute route)
    {
        if (route == null)
        {
            throw new ArgumentNullException(nameof(route));
        }

        await _context.TouristRoutes.AddAsync(route);
    }
}