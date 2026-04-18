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

    public async Task<ShoppingCart?> GetShoppingCartByIdAsync(string userId)
    {
        return await _context.ShoppingCarts.Include(cart => cart.AppUser)
            .Include(cart => cart.CartLineItems)!
            .ThenInclude(item => item.TouristRoute)
            .FirstOrDefaultAsync();
    }

    public async Task<CartLineItem> GetCartLineItemByIdAsync(int itemId)
    {
        return await _context.CartLineItems.FirstOrDefaultAsync(lineItem => lineItem.Id == itemId);
    }

    public async Task CreateShoppingCartItemAsync(CartLineItem cartLineItem)
    {
        await _context.CartLineItems.AddAsync(cartLineItem);
    }

    public void DeleteShoppingCartItem(CartLineItem lineItem)
    {
        _context.CartLineItems.Remove(lineItem);
    }

    public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
    {
        await _context.ShoppingCarts.AddAsync(shoppingCart);
    }
}