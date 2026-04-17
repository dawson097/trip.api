using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Trip.Api.Entities;

namespace Trip.Api.DbContexts;

/// <summary>
/// 数据库上下文配置
/// </summary>
public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<TouristRoute> TouristRoutes { get; set; }

    public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public DbSet<CartLineItem> CartLineItems { get; set; }

    /// <summary>
    /// 从JSON文件中获取实体数据反序列化为集合，通过EFCore以种子数据的形式插入到数据库中
    /// </summary>
    /// <param name="modelBuilder">模型构建器</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var routesFromJson = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
            @"/Assets/tourist-routes.json");
        var routesData = JsonConvert.DeserializeObject<List<TouristRoute>>(routesFromJson)!;
        modelBuilder.Entity<TouristRoute>().HasData(routesData);

        var routePicturesFromJson = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
            @"/Assets/tourist-route-pictures.json");
        var routePicturesData = JsonConvert.DeserializeObject<List<TouristRoutePicture>>(routePicturesFromJson)!;
        modelBuilder.Entity<TouristRoutePicture>().HasData(routePicturesData);

        // 更新用户与角色外键
        modelBuilder.Entity<AppUser>(appUser =>
            appUser.HasMany(user => user.UserRoles).WithOne().HasForeignKey(userRole => userRole.UserId).IsRequired());
    }
}