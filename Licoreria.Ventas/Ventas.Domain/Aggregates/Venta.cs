using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Domain.Aggregates
{
    public class Venta
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Descuento { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Comentarios { get; private set; }
        public string Estado { get; private set; }

        public Venta(Guid id, Guid clienteId, decimal monto,string comentarios)
        {
            Id = id;
            ClienteId = clienteId;
            Monto = monto;
            Fecha = DateTime.UtcNow;
            Comentarios = comentarios;
            Estado = "ACTIVA";
        }

    }
}
