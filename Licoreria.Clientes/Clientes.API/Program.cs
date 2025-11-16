using Clientes.Application.Implementations;
using Clientes.Application.Interfaces;
using Clientes.Domain.Interfaces;
using Clientes.Infrastructure.ExternalServices;
using Clientes.Infrastructure.Messaging;
using Clientes.Infrastructure.Persistence;
using Clientes.Infrastructure.Persistence.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);
var corsConfiguration = "ClientesAPI";


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

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddHostedService<KafkaConsumerHostedService>();

builder.Services.AddDbContext<ClienteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientesDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://auth-server:8080/realms/licoreria-microservices";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,            
            ValidateIssuer = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClienteDbContext>();
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
