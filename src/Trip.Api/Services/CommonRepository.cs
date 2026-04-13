using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class CommonRepository : ICommonRepository
{
    private readonly AppDbContext _context;

    public CommonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RoutesExitsAsync(Guid routeId)
    {
        return await _context.TouristRoutes.AnyAsync(route => route.Id == routeId);
    }

    public async Task<bool> PicturesExitsAsync(int pictureId)
    {
        return await _context.TouristRoutePictures.AnyAsync(picture => picture.Id == pictureId);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}