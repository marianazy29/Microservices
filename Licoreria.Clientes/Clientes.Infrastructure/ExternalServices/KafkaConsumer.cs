using Clientes.Application.Implementations;
using Clientes.Application.Interfaces;
using Clientes.Domain.IntegrationEvents;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.ExternalServices
{
    public class KafkaConsumer 
    {
        private readonly KafkaConsumerSettings _settings;
        
        /*private readonly KafkaConsumerSettings _settings;
        private readonly IServiceProvider _serviceProvider;
        string Topic = "ventas";

        public KafkaConsumer(IOptions<KafkaConsumerSettings> settings, IServiceProvider serviceProvider)
        {
            _settings = settings.Value;
            _serviceProvider = serviceProvider;
        }

        public async Task ConsumirAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _settings.BootstrapServers,
                GroupId = _settings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(Topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(cancellationToken);
                        if (result != null)
                        {
                            var evento = JsonSerializer.Deserialize<VentaCreadaEvent>(result.Message.Value);
                            if (evento == null)
                            {
                                Console.WriteLine("Evento deserializado es null");
                                continue;
                            }

                            if (evento.Tipo != "VENTA_CREADA")
                            {
                                Console.WriteLine($"Evento ignorado por tipo: {evento.Tipo}");
                                continue;
                            }

                            Console.WriteLine($"Evento recibido: Cliente {evento.ClienteId} - Monto {evento.MontoTotal}");

                            using var scope = _serviceProvider.CreateScope();
                            var clienteService = scope.ServiceProvider.GetService<IClienteService>();

                            if (clienteService == null)
                            {
                                Console.WriteLine("No se pudo obtener IClienteService");
                                continue;
                            }

                            await clienteService.SumarPuntos(evento.ClienteId, evento.MontoTotal);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error procesando evento Kafka: {ex.Message}");
                       
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }*/
        private readonly ConsumerConfig _config;
        private readonly string _topic = "ventas";

        public KafkaConsumer(IOptions<KafkaConsumerSettings> settings)
        {
            _settings = settings.Value;
          
            _config = new ConsumerConfig
            {
                BootstrapServers = _settings.BootstrapServers,
                GroupId = _settings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async Task ConsumirAsync(Func<VentaCreadaEvent, Task> onMessage, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(cancellationToken);
                    if (result != null)
                    {
                        var evento = JsonSerializer.Deserialize<VentaCreadaEvent>(result.Message.Value);
                        await onMessage(evento);
                        
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
