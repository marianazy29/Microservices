using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Domain.Events;

namespace Ventas.Infrastructure.ExternalServices
{
    public class KafkaProducer : IKafkaProducer
    {
       
        private readonly KafkaProducerSettings _settings;

        public KafkaProducer(IOptions<KafkaProducerSettings> options)
        {
            _settings = options.Value;
        }

        public async Task PublicarVentaCreadaAsync(VentaCreadaEvent evento)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _settings.BootstrapServers
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            var message = new Message<string, string>
            {
                Key = evento.ClienteId.ToString(),
                Value = System.Text.Json.JsonSerializer.Serialize(evento)
            };

            await producer.ProduceAsync("ventas", message);
        }

        
    }
}
