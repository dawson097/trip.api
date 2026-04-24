using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Entities;

namespace Trip.Api.Extensions;

/// <summary>
/// 用户种子生成扩展
/// </summary>
public static class UserDataSeederExtensions
{
    public static async Task DataSeedAsync(this IHost host)
    {
        // 创建服务作用域
        using var scope = host.Services.CreateScope();
        // 基于作用域创建服务
        var services = scope.ServiceProvider;

        try
        {
            // 获取数据库上下文
            var context = services.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var adminEmail = "admin@trip.com";
            var adminUserId = "165af052-092e-37a7-1891-588b2f1d3cac";
            var adminRoleId = "1bbb725c-a457-21ea-fa29-62dd8a9bce16";

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    Id = adminUserId,
                    UserName = adminEmail,
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<AppDbContext>>();
            logger.LogError(ex, "初始化数据库种子数据时发生错误");
        }
    }
}
