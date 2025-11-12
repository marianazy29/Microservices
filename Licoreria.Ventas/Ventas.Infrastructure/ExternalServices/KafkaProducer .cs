using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Ventas.Application.Interfaces;
using Ventas.Domain.Events;

namespace Ventas.Infrastructure.ExternalServices
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private const string Topic = "ventas";

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka-broker:29092"
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishVentaCreadaAsync(VentaCreadaEvent evento)
        {
            var mensaje = JsonSerializer.Serialize(evento);
            var message = new Message<string, string> { Key = evento.ClienteId.ToString(), Value = mensaje };

            await _producer.ProduceAsync(Topic, message);
        }
    }
}
