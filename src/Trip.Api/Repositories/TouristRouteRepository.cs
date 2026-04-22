using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Helpers;
using Trip.Api.Repositories.Interfaces;

namespace Trip.Api.Repositories;

public class TouristRouteRepository(AppDbContext context)
    : CommonRepository<TouristRoute>(context), ITouristRouteRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<TouristRoute> GetAllRoutesWithQuery(string keyword, string ratingType, int? ratingValue)
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
                _ => queryRes.Where(route => (int)route.Rating! == ratingValue)
            };
        }

        return queryRes;
    }

    public async Task<TouristRoute> GetRouteByIdAsync(Guid routeId)
    {
        return (await _context.TouristRoutes.Include(route => route.TouristRoutePictures)
            .FirstOrDefaultAsync(route => route.Id == routeId))!;
    }

    public async Task<IEnumerable<TouristRoute>> GetRoutesByIdsAsync(IEnumerable<Guid> routeIds)
    {
        return await _context.TouristRoutes.Where(route => routeIds.Contains(route.Id)).ToListAsync();
    }

    public async Task CreateRouteAsync(TouristRoute route)
    {
        if (route == null)
        {
            throw new ArgumentNullException(nameof(route));
        }

        await _context.TouristRoutes.AddAsync(route);
    }

    public void DeleteRoute(TouristRoute route)
    {
        _context.TouristRoutes.Remove(route);
    }

    public void DeleteRoutes(IEnumerable<TouristRoute> routes)
    {
        _context.TouristRoutes.RemoveRange(routes);
    }

    public async Task<PaginationHelper<TouristRoute>> GetAllRoutesAsync(string keyword, string ratingType,
        int? ratingValue, int pageSize, int pageNumber)
    {
        var queryRes = GetAllRoutesWithQuery(keyword, ratingType, ratingValue);

        return await PaginationHelper<TouristRoute>.CreatePaginationAsync(pageNumber, pageSize, queryRes);
    }
}