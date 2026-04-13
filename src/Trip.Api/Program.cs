var builder = WebApplication.CreateBuilder(args);

// 注册控制器路由服务
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // 开发环境下使用开发者异常页面
    app.UseDeveloperExceptionPage();
}

// 映射控制器路由
app.MapControllers();

app.Run();
