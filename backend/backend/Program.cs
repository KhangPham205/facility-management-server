using backend.Data;
using backend.Middlewares;
using backend.Models;
using backend.Repositories.Implements;
using backend.Repositories.Interfaces;
using backend.Services.Implements;
using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Plainquire.Filter;
using Plainquire.Filter.Mvc;
using Plainquire.Sort;
using Plainquire.Sort.Mvc;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddTransient<JwtMiddleware>();
builder.Services.AddHttpContextAccessor();

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<Microsoft.AspNetCore.Identity.IPasswordHasher<User>, Microsoft.AspNetCore.Identity.PasswordHasher<User>>();

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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITransferRequestRepository, TransferRequestRepository>();
builder.Services.AddScoped<ITransferVoucherRepository, TransferVoucherRepository>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
builder.Services.AddScoped<IMaintenanceVoucherRepository, MaintenanceVoucherRepository>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

builder.Services.AddScoped<IRepairRequestRepository, RepairRequestRepository>();
builder.Services.AddScoped<IRepairVoucherRepository, RepairVoucherRepository>();
builder.Services.AddScoped<IRepairService, RepairService>();

builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();

builder.Services.AddScoped<IEquipmentCategoryRepository, EquipmentCategoryRepository>();
builder.Services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();

builder.Services.AddScoped<ICriteriaRepository, CriteriaRepository>();
builder.Services.AddScoped<ICriteriaService, CriteriaService>();

builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddScoped<IBorrowService, BorrowService>();

builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IBuildingService, BuildingService>();

builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IFloorService, FloorService>();

builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IExternalUnitRepository, ExternalUnitRepository>();
builder.Services.AddScoped<IExternalUnitService, ExternalUnitService>();

builder.Services.AddScoped<IFundSourceRepository, FundSourceRepository>();
builder.Services.AddScoped<IFundSourceService, FundSourceService>();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IRoomBookingRepository, RoomBookingRepository>();
builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();

builder.Services.AddScoped<IImportRequestDetailRepository, ImportRequestDetailRepository>();
builder.Services.AddScoped<IImportRequestDetailService, ImportRequestDetailService>();

builder.Services.AddScoped<IImportRequestRepository, ImportRequestRepository>();
builder.Services.AddScoped<IImportRequestService, ImportRequestService>();

builder.Services.AddScoped<IImportVoucherDetailRepository, ImportVoucherDetailRepository>();
builder.Services.AddScoped<IImportVoucherDetailService, ImportVoucherDetailService>();

builder.Services.AddScoped<IImportVoucherRepository, ImportVoucherRepository>();
builder.Services.AddScoped<IImportVoucherService, ImportVoucherService>();

builder.Services.AddScoped<ILiquidateRequestDetailRepository, LiquidateRequestDetailRepository>();
builder.Services.AddScoped<ILiquidateRequestDetailService, LiquidateRequestDetailService>();

builder.Services.AddScoped<ILiquidateRequestRepository, LiquidateRequestRepository>();
builder.Services.AddScoped<ILiquidateRequestService, LiquidateRequestService>();

builder.Services.AddScoped<ILiquidateVoucherDetailRepository, LiquidateVoucherDetailRepository>();
builder.Services.AddScoped<ILiquidateVoucherDetailService, LiquidateVoucherDetailService>();

builder.Services.AddScoped<ILiquidateVoucherRepository, LiquidateVoucherRepository>();
builder.Services.AddScoped<ILiquidateVoucherService, LiquidateVoucherService>();

builder.Services.AddScoped<IAuditDetailRepository, AuditDetailRepository>();
builder.Services.AddScoped<IAuditDetailService, AuditDetailService>();

builder.Services.AddScoped<IInventoryAuditRepository, InventoryAuditRepository>();
builder.Services.AddScoped<IInventoryAuditService, InventoryAuditService>();

builder.Services.AddScoped<IPeriodicAuditRepository, PeriodicAuditRepository>();
builder.Services.AddScoped<IPeriodicAuditService, PeriodicAuditService>();

builder.Services.AddScoped<IAreaRepository, AreaRepository>();

builder.Services.AddSingleton<JwtUtils>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Facility API", Version = "v1" });

    option.OperationFilter<PlainquireFilterOperationFilter>();

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


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddFilterSupport().AddSortSupport();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Cho phép frontend này
                  .AllowAnyHeader()                   // Cho phép mọi Header
                  .AllowAnyMethod();                  // Cho phép GET, POST, PUT, DELETE...
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataApplicationDbContext>();
        context.Database.Migrate();
        Console.WriteLine("--> Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Could not migrate database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();