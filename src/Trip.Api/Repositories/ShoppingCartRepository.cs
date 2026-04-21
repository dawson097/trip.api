using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;

namespace Trip.Api.Repositories;

public class ShoppingCartRepository(AppDbContext context)
    : CommonRepository<ShoppingCart>(context), IShoppingCartRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ShoppingCart> GetCartByUserIdAsync(string userId)
    {
        return (await _context.ShoppingCarts.Include(cart => cart.AppUser)
            .Include(cart => cart.CartLineItems)!
            .ThenInclude(item => item.TouristRoute)
            .Where(cart => cart.UserId == userId)
            .FirstOrDefaultAsync())!;
    }

    public async Task CreateCartAsync(ShoppingCart shoppingCart)
    {
        await _context.ShoppingCarts.AddAsync(shoppingCart);
    }
}