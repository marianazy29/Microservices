using Clientes.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.Messaging
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IKafkaConsumer _consumer;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IKafkaConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Iniciando Kafka consumer cliente");
            await _consumer.ConsumirAsync(stoppingToken);
        }
    }
}
