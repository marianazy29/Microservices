using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Application.Request;
using Ventas.Domain.Aggregates;
using Ventas.Domain.Interfaces;

namespace Ventas.Application.Implementations
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
       

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
           
        }

        public async Task RegistrarVenta(DtoRequestVenta requestVenta)
        {
            var venta = new Venta(requestVenta.UsuarioId, requestVenta.ClienteId, requestVenta.Comentarios);

            foreach (var d in requestVenta.Detalles)
            {
                venta.AgregarDetalle(d.ProductoId, d.Cantidad, d.Precio,d.Descuento);
            }

            await _ventaRepository.Add(venta);

            // Calcular puntos: por cada 50 Bs suma 10 puntos
            decimal monto = Convert.ToDecimal(venta.MontoTotal);
            int bloques = (int)Math.Floor(monto / 50m);
            int puntos = bloques * 10;

            // Invocar microservicio Clientes para sumar puntos
            //await _clienteClient.SumarPuntosAsync(clienteId, puntos);
        }
    }
}
