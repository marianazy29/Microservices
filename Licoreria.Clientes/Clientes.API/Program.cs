using Clientes.Application.Implementations;
using Clientes.Application.Interfaces;
using Clientes.Domain.Interfaces;
using Clientes.Infrastructure.Persistence;
using Clientes.Infrastructure.Persistence.Implementations;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<ClienteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientesDB"));
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
