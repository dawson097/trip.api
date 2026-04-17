using System.ComponentModel.DataAnnotations;

namespace Trip.Api.Dtos.AppUser;

/// <summary>
/// 用户注册DTO
/// </summary>
public class AppUserRegisterDto
{
    [Required(ErrorMessage = "邮箱不应为空")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "邮箱不应为空")]
    public string Password { get; set; } = string.Empty;

    [Required, Compare(nameof(Password), ErrorMessage = "密码前后输入不一致")]
    public string ConfirmPassword { get; set; } = string.Empty;
}