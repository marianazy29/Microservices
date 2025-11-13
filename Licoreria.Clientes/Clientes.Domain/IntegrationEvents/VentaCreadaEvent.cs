using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.IntegrationEvents
{
    public class VentaCreadaEvent
    {
        public Guid ClienteId { get; }
        public decimal MontoTotal { get; }
        public string Tipo { get; }

        public VentaCreadaEvent(Guid clienteId, decimal montoTotal)
        {
            ClienteId = clienteId;
            MontoTotal = montoTotal;
            Tipo = "VENTA_CREADA";
        }
    }
}
