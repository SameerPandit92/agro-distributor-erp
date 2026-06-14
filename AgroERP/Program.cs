using AgroERP.Application.Interfaces;
using AgroERP.Application.Services;
using AgroERP.Application.Validators.Retailer;
using AgroERP.Authorization;
using AgroERP.Middleware;
using AgroERP.Persistence.Context;
using AgroERP.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt",rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder =
    WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services
.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(
        "fixed",
        config =>
        {
            config.PermitLimit = 100;
            config.Window =
                TimeSpan.FromMinutes(1);
            config.QueueLimit = 0;
            config.QueueProcessingOrder =
                QueueProcessingOrder
                .OldestFirst;
        });

    options.RejectionStatusCode =
        StatusCodes
        .Status429TooManyRequests;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services
.AddValidatorsFromAssemblyContaining
<CreateRetailerValidator>();

builder.Services
.AddEndpointsApiExplorer();

builder.Services
.AddDbContext<ApplicationDbContext>(
options =>
options.UseSqlServer(
builder.Configuration
.GetConnectionString(
"DefaultConnection")));


// JWT Authentication
builder.Services
.AddAuthentication(
JwtBearerDefaults
.AuthenticationScheme)

.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey =
                true,

            ValidIssuer =
                builder.Configuration
                ["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration
                ["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                    builder.Configuration
                    ["Jwt:Key"]!))
        };
});


// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Sale:Create", policy =>
    {
        policy.Requirements.Add( new PermissionRequirement("Sale","Create"));
    });
});


// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "AgroERP API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer",
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
        "Enter JWT Token"
    });

    options.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =
                    new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
            },
            Array.Empty<string>()
        }
    });
});


// Dependency Injection
builder.Services.AddScoped<IRetailerRepository,RetailerRepository>();
builder.Services.AddScoped<IRetailerService,RetailerService>();
builder.Services.AddScoped<ISaleRepository,SaleRepository>();

builder.Services
.AddScoped<
ISaleService,
SaleService>();

builder.Services
.AddScoped<
IPaymentCollectionRepository,
PaymentCollectionRepository>();

builder.Services
.AddScoped<
IPaymentCollectionService,
PaymentCollectionService>();

builder.Services
.AddScoped<
ILedgerRepository,
LedgerRepository>();

builder.Services
.AddScoped<
ILedgerService,
LedgerService>();

builder.Services
.AddScoped<
IProductRepository,
ProductRepository>();

builder.Services
.AddScoped<
IProductService,
ProductService>();

builder.Services
.AddScoped<
IReportRepository,
ReportRepository>();

builder.Services
.AddScoped<
IReportService,
ReportService>();

builder.Services
.AddScoped<
IStaffRepository,
StaffRepository>();

builder.Services
.AddScoped<
IStaffService,
StaffService>();

builder.Services
.AddScoped<
IStaffAttendanceRepository,
StaffAttendanceRepository>();

builder.Services
.AddScoped<
IStaffAttendanceService,
StaffAttendanceService>();

builder.Services
.AddScoped<
IStaffLeaveRepository,
StaffLeaveRepository>();

builder.Services
.AddScoped<
IStaffLeaveService,
StaffLeaveService>();

builder.Services
.AddScoped<
ISalaryService,
SalaryService>();

builder.Services
.AddScoped<
IDashboardRepository,
DashboardRepository>();

builder.Services
.AddScoped<
IDashboardService,
DashboardService>();

builder.Services
.AddScoped<
IAuthRepository,
AuthRepository>();

builder.Services
.AddScoped<
IAuthService,
AuthService>();

builder.Services
.AddScoped<
IAuthorizationHandler,
PermissionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware
<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(
"AllowFrontend");

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



//using AgroERP.Application.Interfaces;
//using AgroERP.Application.Services;
//using AgroERP.Application.Validators.Retailer;
//using AgroERP.Authorization;
//using AgroERP.Middleware;
//using AgroERP.Persistence.Context;
//using AgroERP.Persistence.Repositories;
//using FluentValidation;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.RateLimiting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Serilog;
//using System.Threading.RateLimiting;

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("Logs/log-.txt",rollingInterval:RollingInterval.Day)
//    .CreateLogger();


//var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseSerilog();
//// Add services to container
//builder.Services.AddControllers();
//// Add RateLimiter
//builder.Services
//.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter(
//        "fixed",
//        config =>
//        {
//            config.PermitLimit = 100;

//            config.Window =
//                TimeSpan
//                .FromMinutes(1);

//            config.QueueLimit = 0;

//            config.QueueProcessingOrder =
//                QueueProcessingOrder
//                .OldestFirst;
//        });

//    options.RejectionStatusCode =
//        StatusCodes
//        .Status429TooManyRequests;
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(
//        "AllowFrontend",
//        policy =>
//        {
//            policy
//                .AllowAnyOrigin()
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//        });
//});

//builder.Services.AddValidatorsFromAssemblyContaining<CreateRetailerValidator>();
//builder.Services.AddEndpointsApiExplorer();


//// SQL Connection
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Sale:Create",
//    policy =>
//    {
//        policy.Requirements
//        .Add(
//        new PermissionRequirement("Sale", "Create"));
//    });
//});

//// Dependency Injection
//builder.Services.AddScoped <IRetailerRepository, RetailerRepository>();
//builder.Services.AddScoped <IRetailerService, RetailerService>();

//builder.Services.AddScoped <ISaleRepository,SaleRepository>();
//builder.Services.AddScoped <ISaleService, SaleService>();

//builder.Services.AddScoped <IPaymentCollectionRepository, PaymentCollectionRepository>();
//builder.Services.AddScoped <IPaymentCollectionService, PaymentCollectionService>();

//builder.Services.AddScoped <ILedgerService,LedgerService>();
//builder.Services.AddScoped<ILedgerRepository,LedgerRepository>();

//builder.Services.AddScoped<IProductRepository,ProductRepository>();
//builder.Services.AddScoped<IProductService,ProductService>();

//builder.Services.AddScoped<IReportRepository, ReportRepository>();
//builder.Services.AddScoped<IReportService, ReportService>();

//builder.Services.AddScoped<IStaffRepository,StaffRepository>();
//builder.Services.AddScoped<IStaffService,StaffService>();

//builder.Services.AddScoped<IStaffAttendanceRepository,StaffAttendanceRepository>();
//builder.Services.AddScoped<IStaffAttendanceService, StaffAttendanceService>();

//builder.Services.AddScoped<IStaffLeaveRepository, StaffLeaveRepository>();
//builder.Services.AddScoped<IStaffLeaveService,StaffLeaveService>();

//builder.Services.AddScoped <ISalaryService,SalaryService>();

//builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
//builder.Services.AddScoped<IDashboardService,DashboardService>();

//builder.Services.AddScoped<IAuthRepository,AuthRepository>();
//builder.Services.AddScoped<IAuthService,AuthService>();


//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer",
//        new OpenApiSecurityScheme
//        {
//            Name = "Authorization",
//            Type = SecuritySchemeType.Http,
//            Scheme = "bearer",
//            BearerFormat = "JWT",
//            In = ParameterLocation.Header,
//            Description =
//            "Enter JWT Token"
//        });

//    options.AddSecurityRequirement(
//        new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference =
//                    new OpenApiReference
//                    {
//                        Type = ReferenceType.SecurityScheme,
//                        Id ="Bearer"
//                    }
//                },
//                new string[] {}
//            }
//        });
//});
//builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
//var app = builder.Build();

//// Configure HTTP Pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();
//app.UseCors("AllowFrontend");
//app.UseRateLimiter();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();


