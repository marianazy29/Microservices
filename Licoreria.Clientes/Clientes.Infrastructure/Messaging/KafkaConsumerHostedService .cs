using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clientes.Application.Interfaces;
using Clientes.Domain.IntegrationEvents;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clientes.Infrastructure.Messaging
{
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly ILogger<KafkaConsumerHostedService> _logger;        
        private readonly IServiceProvider _serviceProvider;
        private IConsumer<string, string> _consumer;
        private CancellationTokenSource _cts;

        private const string VENTA_TOPIC_NAME = "ventas";

        public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka-broker:29092",
                GroupId = "cliente-service",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Kafka consumer...");
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Task.Run(() => ConsumeMessages(_cts.Token));
            return Task.CompletedTask;
        }

        private async Task ConsumeMessages(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(VENTA_TOPIC_NAME);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(cancellationToken);
                    if (result != null)
                    {
                        _logger.LogInformation($"Message received from topic {VENTA_TOPIC_NAME}: {result.Message.Value}");

                        try
                        {
                            var ventaEvent = JsonSerializer.Deserialize<VentaCreadaEvent>(result.Message.Value);
                            if (ventaEvent != null)
                            {
                                using (var scope = _serviceProvider.CreateScope())
                                {
                                    var clienteService = scope.ServiceProvider.GetRequiredService<IClienteService>();
                                    _logger.LogInformation($"Llamando a SumarPuntos para ClienteId={ventaEvent.ClienteId}, MontoTotal={ventaEvent.MontoTotal}");
                                    await clienteService.SumarPuntos(ventaEvent.ClienteId, ventaEvent.MontoTotal);
                                    _logger.LogInformation($"Terminó SumarPuntos para ClienteId={ventaEvent.ClienteId}");
                                    
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error processing message");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                _consumer.Close();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Kafka consumer...");
            _cts.Cancel();
            return Task.CompletedTask;
        }
    }
}
