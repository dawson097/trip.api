using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Dtos.AppUser;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Controllers;

/// <summary>
/// 用户鉴权控制器路由
/// </summary>
[ApiController, Route("api/auth")]
public class AppUserAuthenticateController(IAppUserService userService) : ControllerBase
{
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] AppUserLoginDto userLoginDto)
    {
        var loginRes = await userService.CheckLoginAsync(userLoginDto);

        if (!loginRes.Succeeded)
        {
            return BadRequest("登录失败!");
        }

        var tokenStr = await userService.GetTokenStringAsync(userLoginDto);

        return Ok(tokenStr);
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] AppUserRegisterDto userRegisterDto)
    {
        var (registerRes, user) = await userService.RegisterAsync(userRegisterDto);

        if (!registerRes.Succeeded)
        {
            return BadRequest("注册失败!");
        }

        await userService.CreateShopCart(user);

        return Ok("注册成功!");
    }
}