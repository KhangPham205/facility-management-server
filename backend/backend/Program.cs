using backend.Data;
using backend.Middlewares;
using backend.Models.TaiKhoan;
using backend.Repositories.Implements;
using backend.Repositories.Interfaces;
using backend.Services.AuthService;
using backend.Services.RoomService;
using backend.Services.FloorService;
using backend.Services.BuildingService;
using backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using backend.Services.RoomTypeService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddTransient<JwtMiddleware>();

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<Microsoft.AspNetCore.Identity.IPasswordHasher<TaiKhoan>, Microsoft.AspNetCore.Identity.PasswordHasher<TaiKhoan>>();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"] ?? string.Empty;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Register services
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<JwtUtils>();

builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IBuildingService, BuildingService>();

builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IFloorService, FloorService>();

builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Facility API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();