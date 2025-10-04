using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using Trip.Api.Models;

namespace Trip.Api.DbContexts;

/// <summary>
/// 数据库上下文管理
/// </summary>
public class TripDbContext : DbContext
{
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
    { }

    public DbSet<TouristRoute> TouristRoutes { get; set; }

    public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }

    /// <summary>
    /// 从Json文件中获取数据后将数据插入数据表中
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var routesJson = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            + @"/Assets/tourist-routes.json");
        var routesData = JsonConvert.DeserializeObject<IEnumerable<TouristRoute>>(routesJson);
        modelBuilder.Entity<TouristRoute>().HasData(routesData);

        var routePicturesJson = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            + @"/Assets/tourist-route-pictures.json");
        var routePicturesData = JsonConvert.DeserializeObject<IEnumerable<TouristRoutePicture>>(routePicturesJson);
        modelBuilder.Entity<TouristRoutePicture>().HasData(routePicturesData);
    }
}