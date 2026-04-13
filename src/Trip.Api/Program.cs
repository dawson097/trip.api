using Mapster;
using Microsoft.EntityFrameworkCore;
using Trip.Api.Configs;
using Trip.Api.DbContexts;
using Trip.Api.Services;
using Trip.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 注册控制器路由服务
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddXmlDataContractSerializerFormatters(); // 实现内容协商

// 注册仓储服务
builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
builder.Services.AddTransient<ITouristRoutePictureRepository, TouristRoutePictureRepository>();

// 注册数据库上下文连接配置服务
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NpgSql");

    options.UseNpgsql(connectionString);
});

// 注册Mapster映射
builder.Services.AddMapster();

MapsterConfig.Configure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // 开发环境下使用开发者异常页面
    app.UseDeveloperExceptionPage();
}

// 映射控制器路由
app.MapControllers();

app.Run();