using Microsoft.EntityFrameworkCore;
using Ventas.Application.Implementations;
using Ventas.Application.Interfaces;
using Ventas.Domain.Interfaces;
using Ventas.Infrastructure.Implementations;
using Ventas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var corsConfiguration = "VentasAPI";

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, x =>
    {
        x.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IVentaService, VentaService>();

builder.Services.AddDbContext<VentaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VentasDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(corsConfiguration);

app.MapControllers();

app.Run();
