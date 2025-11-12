using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces
{
    public interface IKafkaConsumer
    {
        public Task ConsumirAsync(CancellationToken cancellationToken);
    }
}
