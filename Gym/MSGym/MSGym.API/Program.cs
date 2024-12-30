using AutoMapper;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSGym.API.ActionFilters;
using MSGym.Application.Consumers;
using MSGym.Application.Interfaces;
using MSGym.Application.Mappings;
using MSGym.Application.Services;
using MSGym.Domain.DTOs;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Interfaces.UnitOfWork;
using MSGym.Domain.ModelErrors;
using MSGym.Domain.Notifications;
using MSGym.Domain.Services;
using MSGym.Domain.Validators;
using MSGym.Infrastructure.Data;
using MSGym.Infrastructure.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new GymMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelErrorFilter>();
    options.Filters.Add<NotificationFilter>();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLazyLoadingProxies());

// Add Notification
builder.Services.AddScoped<NotificationContext>();

// Add Model Errors
builder.Services.AddScoped<ModelErrorsContext>();

// Add Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Domain Services
builder.Services.AddScoped<EntityValidationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGymService, GymService>();

// Add Domain Validators
builder.Services.AddScoped<IValidator<GymCreateDTO>, GymCreateDTOValidator>();

// Add App Services
builder.Services.AddScoped<IGymAppService, GymAppService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// MassTransit / RabbitMQ
builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();

    // Consumers

    configure.AddConsumer<CreateUserConsumer>();

    //

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]!), h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
