using Clientes.Application.Implementations;
using Clientes.Application.Interfaces;
using Clientes.Domain.IntegrationEvents;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.ExternalServices
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly KafkaConsumerSettings _settings;
        string Topic = "ventas";
        public KafkaConsumer(IOptions<KafkaConsumerSettings> settings)
        {
            _settings = settings.Value;
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
                    var result = consumer.Consume(cancellationToken);
                    if (result != null)
                    {
                        var evento = JsonSerializer.Deserialize<VentaCreadaEvent>(result.Message.Value);
                        Console.WriteLine($"Evento recibido: Cliente {evento.ClienteId} - Monto {evento.MontoTotal}");


                      //  await  _clienteService.SumarPuntos(evento.ClienteId, evento.MontoTotal);
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
