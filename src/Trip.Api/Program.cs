using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// 注册控制器路由服务
builder.Services.AddControllers();

// 注册数据库上下文连接配置服务
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NpgSql");

    options.UseNpgsql(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // 开发环境下使用开发者异常页面
    app.UseDeveloperExceptionPage();
}

// 映射控制器路由
app.MapControllers();

app.Run();