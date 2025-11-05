using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Application.Request
{
    public class DtoRequestVenta
    {
        public Guid UsuarioId { get; set; }
        public Guid ClienteId { get; set; }
        public string Comentarios { get; set; }
        public List<DtoRequestDetalleDeVenta> Detalles { get; set; } = new();
    }
}
