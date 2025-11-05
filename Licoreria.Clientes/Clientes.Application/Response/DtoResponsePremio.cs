using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Response
{
    public class DtoResponsePremio
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public Guid ProductoId { get; set; }
        public DateTime Fecha { get; private set; }
    }
}
