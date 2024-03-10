using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MSAuth.API.ActionFilters;
using MSAuth.Application.Mappings;
using MSAuth.Application.Services;
using MSAuth.Domain.IRepositories;
using MSAuth.Domain.Notifications;
using MSAuth.Infrastructure.Data;
using MSAuth.Infrastructure.Repositories;

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
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Notification
builder.Services.AddScoped<NotificationContext>();

// Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppRepository, AppRepository>();
builder.Services.AddScoped<IUserConfirmationRepository, UserConfirmationRepository>();

// Add Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AppService>();
builder.Services.AddScoped<EmailService>();

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
