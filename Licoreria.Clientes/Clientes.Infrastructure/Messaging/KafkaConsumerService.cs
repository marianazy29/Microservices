using Clientes.Application.Interfaces;
using Clientes.Infrastructure.ExternalServices;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly KafkaConsumer _kafkaConsumer;
        private readonly IServiceProvider _serviceProvider;
       
        public KafkaConsumerService(KafkaConsumer kafkaConsumer, IServiceProvider serviceProvider)
        {
            _kafkaConsumer = kafkaConsumer;
            _serviceProvider = serviceProvider;
          
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _kafkaConsumer.ConsumirAsync(async evento =>
            {
                 using var scope = _serviceProvider.CreateScope();
                  var clienteService = scope.ServiceProvider.GetRequiredService<IClienteService>();

                 
                  await clienteService.SumarPuntos(evento.ClienteId, evento.MontoTotal);
                

            }, stoppingToken);
        }
    }
}
