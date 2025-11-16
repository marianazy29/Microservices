using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Application.Request;
using Ventas.Application.Response;
using Ventas.Domain.Aggregates;
using Ventas.Domain.Interfaces;

namespace Ventas.Application.Implementations
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteApiClient _clienteApiClient;
      
        public VentaService(IVentaRepository ventaRepository, IClienteApiClient clienteApiClient)
        {
            _ventaRepository = ventaRepository;
            _clienteApiClient = clienteApiClient;
            
        }

        public async Task<string> RegistrarVenta(DtoRequestVenta requestVenta)
        {
            string estadoVenta = "Venta creada.";

           

            if (!await _clienteApiClient.GetByid(requestVenta.ClienteId))
            {
                return estadoVenta = "El cliente no existe.";
            }

            var venta = new Venta(requestVenta.UsuarioId, requestVenta.ClienteId, requestVenta.Comentarios);

            foreach (var d in requestVenta.Detalles)
            {
                venta.AgregarDetalle(d.ProductoId, d.Cantidad, d.Precio,d.Descuento);
            }

            await _ventaRepository.Add(venta);         
          

            return estadoVenta;

            
        }

        public async Task<ICollection<DtoResponseCliente>> ObtenerClientes()
        {
            
            var clientes = await _clienteApiClient.ListarClientes();
            return clientes;
        }

        
    }
}
