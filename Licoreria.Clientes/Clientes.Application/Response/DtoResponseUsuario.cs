using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Response
{
    public class DtoResponseUsuario
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public decimal PuntosAcumulados { get; set; }
        public List<DtoResponsePremio> Premios { get; set; } = new();
    }
}
