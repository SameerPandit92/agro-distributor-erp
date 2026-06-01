using AgroERP.Application.Interfaces;
using AgroERP.Application.Services;
using AgroERP.Application.Validators.Retailer;
using AgroERP.Persistence.Context;
using AgroERP.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers();

builder.Services
    .AddValidatorsFromAssemblyContaining<CreateRetailerValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Connection
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped <IRetailerRepository, RetailerRepository>();
builder.Services.AddScoped <IRetailerService, RetailerService>();
builder.Services.AddScoped <ISaleRepository,SaleRepository>();
builder.Services.AddScoped <ISaleService, SaleService>();
builder.Services.AddScoped <IPaymentCollectionRepository, PaymentCollectionRepository>();
builder.Services.AddScoped <IPaymentCollectionService, PaymentCollectionService>();

var app = builder.Build();

// Configure HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

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
