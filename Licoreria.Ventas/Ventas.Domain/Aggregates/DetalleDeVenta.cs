using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Domain.Aggregates
{
    public class DetalleDeVenta
    {
        public Guid Id { get; private set; }
        public Guid VentaId { get; private set; }
        public Guid ProductoId { get; private set; }
        public int Cantidad { get; private set; }
        public double Precio { get; private set; }
        public double Descuento { get; private set; }
        public double TotalLinea { get; private set; }
        public string Estado { get; private set; }

        public DetalleDeVenta(Guid ventaId, Guid productoId, int cantidad, double precio, double descuento)
        {
            Id = Guid.NewGuid();
            VentaId = ventaId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Precio = precio;
            Descuento = descuento;
            TotalLinea = (cantidad * precio) -  descuento;
            Estado = "ACTIVO";
        }

        public void CambiarEstado(string estado)
        {
            Estado = estado;
        }

        public void ActualizarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad < 1)
                throw new ArgumentException("Cantidad debe ser mayor que cero.");
            Cantidad = nuevaCantidad;
        }
        public void ActualizarTotalLinea(double cantidad, double precio, double descuento)
        {
            TotalLinea = (cantidad * precio) - descuento; ;
        }
    }
}

