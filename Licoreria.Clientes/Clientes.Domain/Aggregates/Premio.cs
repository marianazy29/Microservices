using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Aggregates
{
    public class Premio
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid ProductoId { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Estado { get; private set; }

        private Premio() { }

        public Premio(Guid clienteId, string descripcion, Guid productoId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            ProductoId = productoId;
            Descripcion = descripcion;
            Fecha = DateTime.Now;
            Estado = "ACTIVO";
        }

        public void Inactivar() => Estado = "INACTIVO";
    }
}
