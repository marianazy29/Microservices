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
        public Guid UsuarioId { get; private set; }
        public Guid ClienteId { get; private set; }
        public DateTime Fecha { get; private set; }
        public double Descuento { get; private set; }
        public double MontoTotal { get; private set; }       
        public string Comentarios { get; private set; }
        public string Estado { get; private set; }

        private readonly List<DetalleDeVenta> _detalles = new();
        public IReadOnlyCollection<DetalleDeVenta> Detalles => _detalles.AsReadOnly();
        public Venta(Guid usuarioId,Guid clienteId,string comentarios)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            ClienteId = clienteId;
            MontoTotal = 0;
            Fecha = DateTime.UtcNow;
            Comentarios = comentarios;
            Estado = "ACTIVA";
        }

        public void AgregarDetalle(Guid productoId, int cantidad, double precio, double descuento)
        {
            if (cantidad < 1)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            var detalle = new DetalleDeVenta(Id, productoId, cantidad, precio, descuento);
            _detalles.Add(detalle);
            ActualizarMontoTotal();
        }

        public void CambiarEstadoDetalle(Guid detalleId, string estado)
        {
            var detalle = _detalles.FirstOrDefault(d => d.Id == detalleId);
            if (detalle == null)
                throw new InvalidOperationException("Detalle no encontrado.");

            detalle.CambiarEstado(estado);
            ActualizarMontoTotal();
        }

        private void ActualizarMontoTotal()
        {
            double descuentoTotal = ActualizarDescuento();
            MontoTotal = _detalles.Where(d => d.Estado == "ACTIVO").Sum(d => d.Precio * d.Cantidad) - descuentoTotal;
        }

        private double ActualizarDescuento()
        {
           return  Descuento = _detalles.Where(d => d.Estado == "ACTIVO").Sum(d => d.Descuento);
        }
    }
}
