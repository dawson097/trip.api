using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync(string userId)
    {
        return await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return (await _context.Orders.Include(order => order.OrderItems)!
            .ThenInclude(order => order.TouristRoute)
            .FirstOrDefaultAsync(order => order.Id == orderId))!;
    }
}