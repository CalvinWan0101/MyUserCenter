using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyUserCenter.Domain;
using MyUserCenter.EFCore;
using MyUserCenter.Service;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<MyUserCenterDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MyUserCenterDbContext"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MyUserMappingProfile));

// PasswordHasher
builder.Services.AddScoped<IPasswordHasher<MyUser>, PasswordHasher<MyUser>>();

// Dependency Injection
builder.Services.AddScoped<IMyUserService, MyUserService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
