using AutoMapper;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSAuth.API.ActionFilters;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Application.Mappings;
using MSAuth.Application.Services;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;
using MSAuth.Domain.Services;
using MSAuth.Domain.Validators;
using MSAuth.Infrastructure.Data;
using MSAuth.Infrastructure.Services;
using MSAuth.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
    mc.AddProfile(new AppMappingProfile());
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

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure your Identity security options here, if needed
});

// Add Notification
builder.Services.AddScoped<NotificationContext>();

// Add Model Errors
builder.Services.AddScoped<ModelErrorsContext>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Domain Services
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserConfirmationService, UserConfirmationService>();


// Add Domain Validators
builder.Services.AddScoped<IValidator<UserCreateDTO>, UserCreateDTOValidator>();

builder.Services.AddScoped<EntityValidationService>();

// Add App Services
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IAppAppService, AppAppService>();
builder.Services.AddScoped<IUserConfirmationAppService, UserConfirmationAppService>();

// Add Infrastructure Services
builder.Services.AddScoped<IEmailService, MockedEmailService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
