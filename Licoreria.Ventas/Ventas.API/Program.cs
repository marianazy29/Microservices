using Microsoft.EntityFrameworkCore;
using Polly;
using Ventas.Application.Implementations;
using Ventas.Application.Interfaces;
using Ventas.Domain.Interfaces;
using Ventas.Infrastructure.ExternalServices;
using Ventas.Infrastructure.Implementations;
using Ventas.Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

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
builder.Services.AddScoped<IClienteApiClient, ClienteApiClient>();

builder.Services.AddDbContext<VentaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VentasDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddHttpClient<IClienteApiClient, ClienteApiClient>(client =>
{
    client.BaseAddress = new Uri("http://cliente-loadbalancer:80/");
})
.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
.AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(corsConfiguration);

app.MapControllers();

app.Run();
