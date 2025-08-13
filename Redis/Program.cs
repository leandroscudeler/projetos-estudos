using ApiRedisSample.Data;
using ApiRedisSample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQLite local file "local.db" in app root
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite") ?? "Data Source=local.db"));

// Redis service (singleton ConnectionMultiplexer inside)
builder.Services.AddSingleton<RedisService>();

// Business service
builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB exists and seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new ApiRedisSample.Models.Product { Name = "Notebook", Price = 3500m },
            new ApiRedisSample.Models.Product { Name = "Mouse", Price = 150m },
            new ApiRedisSample.Models.Product { Name = "Teclado", Price = 220m }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
