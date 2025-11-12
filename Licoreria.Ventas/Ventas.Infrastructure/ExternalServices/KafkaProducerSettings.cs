using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ventas.Infrastructure.ExternalServices
{
    public class KafkaProducerSettings
    {
        public string BootstrapServers { get; set; } = "";
        public string KeySerializer { get; set; } = "";
        public string ValueSerializer { get; set; } = "";
    }
}
