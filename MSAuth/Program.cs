using AutoMapper;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MSAuth.API.ActionFilters;
using MSAuth.API.Middlewares;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Application.Mappings;
using MSAuth.Application.Services;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Persistence.CachedRepositories;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;
using MSAuth.Domain.Services;
using MSAuth.Domain.Validators;
using MSAuth.Infrastructure.Data;
using MSAuth.Infrastructure.Persistence.CachedRepositories;
using MSAuth.Infrastructure.Services;
using MSAuth.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotificationFilter>();
    options.Filters.Add<ModelErrorFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Notification
builder.Services.AddScoped<NotificationContext>();

// Add Model Errors
builder.Services.AddScoped<ModelErrorsContext>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Redis Cache
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration.GetConnectionString("Redis")!;
    redisOptions.Configuration = connection;
});

// Add Domain Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserConfirmationService, UserConfirmationService>();


// Add Domain Validators
builder.Services.AddScoped<IValidator<UserCreateDTO>, UserCreateDTOValidator>();

builder.Services.AddScoped<EntityValidationService>();

// Add App Services
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IUserConfirmationAppService, UserConfirmationAppService>();

// Add Infrastructure Services
builder.Services.AddScoped<IEmailService, MockedEmailService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Add Caching Repositories
builder.Services.AddScoped<IRefreshTokenCachedRepository, RefreshTokenCachedRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("HangfireConnection");
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();

var app = builder.Build();

// Add Middlewares
app.UseMiddleware<AppKeyValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

//app.UseHttpsRedirection(); // For testing purposes was commented out

app.UseAuthorization();

app.MapControllers();

app.Run();
