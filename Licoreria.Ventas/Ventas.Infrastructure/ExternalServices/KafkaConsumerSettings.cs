using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Infrastructure.ExternalServices
{
    public class KafkaConsumerSettings
    {
        public string GroupId { get; set; } = "";
        public string BootstrapServers { get; set; } = "";
        public string KeyDeserializer { get; set; } = "";
        public string ValueDeserializer { get; set; } = "";
    }
}
