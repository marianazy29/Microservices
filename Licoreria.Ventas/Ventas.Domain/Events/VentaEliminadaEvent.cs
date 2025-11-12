using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Domain.Events
{
    public class VentaEliminadaEvent
    {
        public Guid ClienteId { get; }
        public double MontoTotal { get; }

        public VentaEliminadaEvent(Guid clienteId, double montoTotal)
        {
            ClienteId = clienteId;
            MontoTotal = montoTotal;
        }
    }
}
