using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Trip.Api.Dtos.AppUser;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class AppUserService(
    ICommonRepository<AppUser> commonRepository,
    IShoppingCartRepository cartRepository,
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager,
    IConfiguration configuration)
    : CommonService<AppUser>(commonRepository), IAppUserService
{
    public async Task<SignInResult> CheckLoginAsync(AppUserLoginDto userLoginDto)
    {
        return await signInManager.PasswordSignInAsync(
            userLoginDto.Email,
            userLoginDto.Password,
            false,
            false
        );
    }

    public async Task<string> GetTokenStringAsync(AppUserLoginDto userLoginDto)
    {
        var user = await userManager.FindByEmailAsync(userLoginDto.Email);

        // header
        var signatureAlgorithm = SecurityAlgorithms.HmacSha256;
        // payload
        List<Claim> claims = [new(JwtRegisteredClaimNames.Sub, user!.Id)];
        var roleNames = await userManager.GetRolesAsync(user);

        foreach (var roleName in roleNames)
        {
            var roleClaim = new Claim(ClaimTypes.Role, roleName);
            claims.Add(roleClaim);
        }

        // signature
        var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]!);
        var signingKey = new SymmetricSecurityKey(secretByte);
        var signingCredentials = new SigningCredentials(signingKey, signatureAlgorithm);

        var token = new JwtSecurityToken(
            configuration["Authentication:Issuer"],
            configuration["Authentication:audience"],
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<(IdentityResult, AppUser)> RegisterAsync(AppUserRegisterDto userRegisterDto)
    {
        var user = new AppUser
        {
            UserName = userRegisterDto.Email,
            Email = userRegisterDto.Email
        };

        var registerRes = await userManager.CreateAsync(user, userRegisterDto.Password);

        return (registerRes, user);
    }

    public async Task CreateShopCart(AppUser user)
    {
        var cart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };

        await cartRepository.CreateCartAsync(cart);
        await cartRepository.SaveAsync();
    }
}