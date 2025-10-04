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

    public IEnumerable<TouristRoute> GetAllRoutes(string keyword, string ratingType, int ratingValue)
    {
        IQueryable<TouristRoute> result = _context.TouristRoutes.Include(
            route => route.TouristRoutePictures);

        // 判断keyword是否为空或者含有空值
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            result = result.Where(route => route.Title.Contains(keyword));
        }

        if (ratingValue >= 0)
        {
            result = ratingType switch
            {
                "largerThan" => result.Where(route => route.Rating >= ratingValue),
                "lessThan" => result.Where(route => route.Rating <= ratingValue),
                _ => result.Where(route => route.Rating == ratingValue)
            };
        }

        return result.ToList();
    }

    public TouristRoute GetRouteById(Guid routeId)
    {
        return _context.TouristRoutes.Include(route => route.TouristRoutePictures)
            .FirstOrDefault(route => route.Id == routeId);
    }
}