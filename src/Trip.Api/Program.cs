using System.Text;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Trip.Api.Configs;
using Trip.Api.DbContexts;
using Trip.Api.Entities;
using Trip.Api.Extensions;
using Trip.Api.Services;
using Trip.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 注册JWT鉴权服务
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretByte = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!);

        // 配置token校验参数
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 校验发起者
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            // 校验接收者
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Authentication:Audience"],
            // 校验生命周期
            ValidateLifetime = true,
            // 生成发起者的签名密钥
            IssuerSigningKey = new SymmetricSecurityKey(secretByte)
        };
    });

// 注册控制器路由服务
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()) // 配置JSON命名方式为驼峰式
    .AddXmlDataContractSerializerFormatters() // 实现内容协商
    .ConfigureApiBehaviorOptions(options =>
    {
        // 配置模型状态响应校验工厂
        options.InvalidModelStateResponseFactory = context =>
        {
            // 配置问题信息
            var problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Type = "数据验证",
                Title = "验证错误",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "数据验证错误",
                Instance = context.HttpContext.Request.Path
            };

            // 添加trace id
            problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

            // 配置响应结果实体类型
            return new UnprocessableEntityObjectResult(problemDetail)
            {
                ContentTypes = { "application/problem+json", "application/problem+xml" }
            };
        };
    }); // 配置422请求

// 注册仓储服务
builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
builder.Services.AddTransient<ITouristRoutePictureRepository, TouristRoutePictureRepository>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

// 注册数据库上下文连接配置服务
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NpgSql");

    options.UseNpgsql(connectionString);
});

// 注册身份认证
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>(); // 连接数据库上下文以生成初始化数据表

// 注册Mapster映射
builder.Services.AddMapster();

MapsterConfig.Configure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // 开发环境下使用开发者异常页面
    app.UseDeveloperExceptionPage();
}

// 通过UserDataSeederExtension生成用户
await app.DataSeedAsync();

// 启用用户身份鉴权
app.UseAuthentication();
// 启用用户授权
app.UseAuthorization();

// 映射控制器路由
app.MapControllers();

app.Run();