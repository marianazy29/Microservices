using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Application.Request
{
    public class DtoRequestDetalleDeVenta
    {        
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
       
    }
}
