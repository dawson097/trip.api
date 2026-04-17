using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class ShoppingCartRepository : CommonRepository, IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ShoppingCart?> GetShoppingCartById(string userId)
    {
        return await _context.ShoppingCarts.Include(cart => cart.AppUser)
            .Include(cart => cart.CartLineItems)!
            .ThenInclude(item => item.TouristRoute)
            .FirstOrDefaultAsync();
    }

    public async Task CreateShoppingCart(ShoppingCart shoppingCart)
    {
        await _context.ShoppingCarts.AddAsync(shoppingCart);
    }

    public async Task CreateShoppingCartItem(CartLineItem cartLineItem)
    {
        await _context.CartLineItems.AddAsync(cartLineItem);
    }
}