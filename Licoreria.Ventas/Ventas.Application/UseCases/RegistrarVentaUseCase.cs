using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Interfaces;
using Ventas.Application.Request;
using Ventas.Domain.Aggregates;
using Ventas.Domain.Events;
using Ventas.Domain.Interfaces;

namespace Ventas.Application.UseCases
{
    public class RegistrarVentaUseCase
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteApiClient _clienteApiClient;
        private readonly IKafkaProducer _kafkaProducer;
        public RegistrarVentaUseCase(IVentaRepository ventaRepository, IClienteApiClient clienteApiClient, IKafkaProducer kafkaProducer)
        {
            _ventaRepository = ventaRepository;
            _clienteApiClient = clienteApiClient;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<string> EjecutarAsync(DtoRequestVenta requestVenta)
        {
            if (!await _clienteApiClient.GetByid(requestVenta.ClienteId))
            {
                return "El cliente no existe.";
            }

            var venta = new Venta(requestVenta.UsuarioId, requestVenta.ClienteId, requestVenta.Comentarios);

            foreach (var d in requestVenta.Detalles)
            {
                venta.AgregarDetalle(d.ProductoId, d.Cantidad, d.Precio, d.Descuento);
            }

            await _ventaRepository.Add(venta);

            
            var evento = new VentaCreadaEvent(venta.ClienteId, venta.MontoTotal);

           
            await _kafkaProducer.PublishVentaCreadaAsync(evento);
            return "Venta creada.";
        }
    }
}
