using Microsoft.AspNetCore.Identity;
using Trip.Api.Dtos.AppUser;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

public interface IAppUserService : ICommonService<AppUser>
{
    Task<SignInResult> CheckLoginAsync(AppUserLoginDto userLoginDto);

    Task<string> GetTokenStringAsync(AppUserLoginDto userLoginDto);

    Task<(IdentityResult, AppUser)> RegisterAsync(AppUserRegisterDto userRegisterDto);

    Task CreateShopCart(AppUser user);
}