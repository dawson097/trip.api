var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();