using AgroERP.Application.Interfaces;
using AgroERP.Persistence.Context;
using AgroERP.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sql Connection
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Added the Service
builder.Services.AddScoped<IRetailerRepository,RetailerRepository>();

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
