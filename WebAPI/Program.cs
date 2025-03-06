using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using Application.UseCases.Commands.Users.Register;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services.Auth;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new ArgumentException("Database connection string not found!")));

builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Registering all validators in assembly (not only those for user registering)
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
builder.Services.AddScoped<IUserContext, UserContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(configuration =>
        configuration.SwaggerEndpoint("/openapi/v1.json", "Fifty-Fifty v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();