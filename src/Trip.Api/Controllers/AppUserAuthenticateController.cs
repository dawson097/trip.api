using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Trip.Api.Dtos.AppUser;
using Trip.Api.Entities;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 用户鉴权控制器路由
/// </summary>
[ApiController, Route("api/auth")]
public class AppUserAuthenticateController : ControllerBase
{
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AppUserAuthenticateController(IShoppingCartRepository cartRepository, IConfiguration configuration,
        SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _cartRepository = cartRepository;
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] AppUserLoginDto userLoginDto)
    {
        var loginRes = await _signInManager.PasswordSignInAsync(
            userLoginDto.Email,
            userLoginDto.Password,
            false,
            false
        );

        if (!loginRes.Succeeded)
        {
            return BadRequest("登录失败!");
        }

        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

        // header
        var signatureAlgorithm = SecurityAlgorithms.HmacSha256;
        // payload
        List<Claim> claims = [new(JwtRegisteredClaimNames.Sub, user!.Id)];
        var roleNames = await _userManager.GetRolesAsync(user);

        foreach (var roleName in roleNames)
        {
            var roleClaim = new Claim(ClaimTypes.Role, roleName);
            claims.Add(roleClaim);
        }

        // signature
        var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!);
        var signingKey = new SymmetricSecurityKey(secretByte);
        var signingCredentials = new SigningCredentials(signingKey, signatureAlgorithm);

        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:audience"],
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            signingCredentials
        );

        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenStr);
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] AppUserRegisterDto userRegisterDto)
    {
        var user = new AppUser
        {
            UserName = userRegisterDto.Email,
            Email = userRegisterDto.Email
        };

        var registerRes = await _userManager.CreateAsync(user, userRegisterDto.Password);

        if (!registerRes.Succeeded)
        {
            return BadRequest("注册失败!");
        }

        var shoppingCart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };

        await _cartRepository.CreateShoppingCart(shoppingCart);
        await _cartRepository.SaveAsync();

        return Ok("注册成功!");
    }
}