using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Application.Response
{
    public class DtoResponseVenta
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public double Descuento { get; set; }
        public double MontoTotal { get; set; }
        public string Estado { get; set; }
        public List<DtoResponseDetalleDeVenta> DetalleDeVebta { get; set; } = new();
    }
}
