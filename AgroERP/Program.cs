using AgroERP.Application.Interfaces;
using AgroERP.Application.Services;
using AgroERP.Application.Validators.Retailer;
using AgroERP.Middleware;
using AgroERP.Persistence.Context;
using AgroERP.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRetailerValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Connection
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped <IRetailerRepository, RetailerRepository>();
builder.Services.AddScoped <IRetailerService, RetailerService>();

builder.Services.AddScoped <ISaleRepository,SaleRepository>();
builder.Services.AddScoped <ISaleService, SaleService>();

builder.Services.AddScoped <IPaymentCollectionRepository, PaymentCollectionRepository>();
builder.Services.AddScoped <IPaymentCollectionService, PaymentCollectionService>();

builder.Services.AddScoped <ILedgerService,LedgerService>();
builder.Services.AddScoped<ILedgerRepository,LedgerRepository>();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IStaffRepository,StaffRepository>();
builder.Services.AddScoped<IStaffService,StaffService>();

builder.Services.AddScoped<IStaffAttendanceRepository,StaffAttendanceRepository>();
builder.Services.AddScoped<IStaffAttendanceService, StaffAttendanceService>();

builder.Services.AddScoped<IStaffLeaveRepository, StaffLeaveRepository>();
builder.Services.AddScoped<IStaffLeaveService,StaffLeaveService>();

builder.Services.AddScoped <ISalaryService,SalaryService>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService,DashboardService>();

builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddScoped<IAuthService,AuthService>();

builder.Services.AddSwaggerGen(options =>
{
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
                new string[] {}
            }
        });
});


var app = builder.Build();


// Configure HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


//using AgroERP.Application.Interfaces;
//using AgroERP.Application.Services;
//using AgroERP.Application.Validators.Retailer;
//using AgroERP.Persistence.Context;
//using AgroERP.Persistence.Repositories;
//using FluentValidation;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Services
//    .AddFluentValidationAutoValidation();

//builder.Services
//    .AddValidatorsFromAssemblyContaining
//    <CreateRetailerValidator>();


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Sql Connection
//builder.Services.AddDbContext<ApplicationDbContext>(
//    options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Added the Service
//builder.Services.AddScoped<IRetailerRepository,RetailerRepository>();
//builder.Services.AddScoped<IRetailerService,RetailerService>();

//var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
