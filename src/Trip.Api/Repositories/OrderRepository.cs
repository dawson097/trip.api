using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Helpers;
using Trip.Api.Repositories.Interfaces;

namespace Trip.Api.Repositories;

public class OrderRepository(AppDbContext context) : CommonRepository<Order>(context), IOrderRepository
{
    private readonly AppDbContext _context = context;

    public async Task<PaginationHelper<Order>> GetAllOrdersByUserIdAsync(string userId, int pageSize, int pageNumber)
    {
        var queryRes = _context.Orders.Where(order => order.UserId == userId);

        return await PaginationHelper<Order>.CreatePaginationAsync(pageNumber, pageSize, queryRes);
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return (await _context.Orders.Include(order => order.OrderItems)!
            .ThenInclude(item => item.TouristRoute)
            .FirstOrDefaultAsync(order => order.Id == orderId))!;
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}