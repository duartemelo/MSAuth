using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MSAuth.API.ActionFilters;
using MSAuth.Application.Mappings;
using MSAuth.Application.Services;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using MSAuth.Domain.Services;
using MSAuth.Infrastructure.Data;
using MSAuth.Infrastructure.Repositories;
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
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Notification
builder.Services.AddScoped<NotificationContext>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Domain Services
builder.Services.AddScoped<IAppService, AppService>();

// Add App Services
builder.Services.AddScoped<UserAppService>();
builder.Services.AddScoped<AppAppService>();
builder.Services.AddScoped<EmailAppService>();

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
