using System.ComponentModel.DataAnnotations;

namespace Trip.Api.Dtos.AppUser;

/// <summary>
/// 用户登录DTO
/// </summary>
public class AppUserLoginDto
{
    [Required(ErrorMessage = "邮箱不应为空")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "密码不应为空")]
    public string Password { get; set; } = string.Empty;
}