var builder = WebApplication.CreateBuilder(args);

// 添加路由控制器服务
builder.Services.AddControllers();

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