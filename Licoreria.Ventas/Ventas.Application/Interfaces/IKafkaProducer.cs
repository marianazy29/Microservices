using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Domain.Events;

namespace Ventas.Application.Interfaces
{
    public interface IKafkaProducer
    {
        Task PublicarVentaCreadaAsync(VentaCreadaEvent evento);
    }
}
