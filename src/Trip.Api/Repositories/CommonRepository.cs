using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Repositories.Interfaces;

namespace Trip.Api.Repositories;

public class CommonRepository<T>(AppDbContext context) : ICommonRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<bool> CheckExitsAsync(Expression<Func<T, bool>> entityId)
    {
        return await _dbSet.AnyAsync(entityId);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }
}