using Microsoft.EntityFrameworkCore;
using Polly;
using Ventas.Application.Implementations;
using Ventas.Application.Interfaces;
using Ventas.Application.UseCases;
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
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
builder.Services.AddScoped<RegistrarVentaUseCase>();

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

builder.Services.Configure<KafkaProducerSettings>(
    builder.Configuration.GetSection("Kafka:Producer"));

builder.Services.Configure<KafkaConsumerSettings>(
    builder.Configuration.GetSection("Kafka:Consumer"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VentaDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);

    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            db.Database.Migrate();
            logger.LogInformation("Migraciones aplicadas correctamente.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, $"Intento {i + 1} de {maxRetries}: no se pudo conectar a la base de datos.");
            Thread.Sleep(delay);
        }
    }
}


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
