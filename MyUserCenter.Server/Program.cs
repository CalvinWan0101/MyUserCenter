using Microsoft.EntityFrameworkCore;
using MyUserCenter.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<MyUserCenterDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MyUserCenterDbContext"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
