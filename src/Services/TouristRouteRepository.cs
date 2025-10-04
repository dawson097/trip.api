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

    public IEnumerable<TouristRoute> GetAllRoutes(string keyword)
    {
        IQueryable<TouristRoute> result = _context.TouristRoutes.Include(
            route => route.TouristRoutePictures);

        // 判断keyword是否为空或者含有空值
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            result = result.Where(route => route.Title.Contains(keyword));
        }

        return result.ToList();
    }

    public TouristRoute GetRouteById(Guid routeId)
    {
        return _context.TouristRoutes.Include(route => route.TouristRoutePictures)
            .FirstOrDefault(route => route.Id == routeId);
    }
}