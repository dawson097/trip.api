using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip.Api.DbContexts;
using Trip.Api.Services;
using Trip.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 添加路由控制器服务
builder.Services.AddControllers(options =>
    {
        // 若返回格式不是已知格式，则返回406错误
        options.ReturnHttpNotAcceptable = true;
    }).AddXmlDataContractSerializerFormatters() // 添加XML格式支持
    .ConfigureApiBehaviorOptions(options => // 配置api行为
    {
        // 配置无效模型响应工厂
        options.InvalidModelStateResponseFactory = context =>
        {

            var problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Type = "422 Error",
                Title = "标题或者描述存在问题",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "标题或描述相同，或是其中一项为空值，请检查",
                Instance = context.HttpContext.Request.Path
            };

            problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

            return new UnprocessableEntityObjectResult(problemDetail)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

// 注册仓储服务
builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
builder.Services.AddTransient<ITouristRoutePictureRepository, TouristRoutePictureRepository>();

// 添加数据库上下文配置服务，并连接至数据库
builder.Services.AddDbContext<TripDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

    options.UseSqlServer(connectionString);
});

// 添加AutoMapper映射服务
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// 判断是否在开发环境下
if (app.Environment.IsDevelopment())
{
    // 使用开发者异常页面
    app.UseDeveloperExceptionPage();
}

// 使用路由控制器
app.MapControllers();

app.Run();