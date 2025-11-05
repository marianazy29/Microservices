using Clientes.Application.Interfaces;
using Clientes.Application.Request;
using Clientes.Application.Response;
using Clientes.Domain.Aggregates;
using Clientes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<DtoResponseUsuario?> GetCliente(Guid id)
        {
            var cliente = await _clienteRepository.GetAsync(id);
            if (cliente == null) return null;

            return new DtoResponseUsuario
            {
                Id = cliente.Id,
                NombreCompleto = cliente.NombreCompleto,
                PuntosAcumulados = cliente.PuntosAcumulados.Puntos,
                Premios = cliente.Premios.Select(p => new DtoResponsePremio
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    ProductoId = p.ProductoId
                }).ToList()
            };
        }

        public async Task CrearCliente(DtoRequestCliente request)
        {
            var cliente = new Cliente(request.NombreCompleto);
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task SumarPuntos(Guid clienteId, decimal puntos)
        {
            var cliente = await _clienteRepository.GetAsync(clienteId);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            cliente.AgregarPuntos(puntos);
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task EntregarPremio(Guid clienteId, string descripcion, Guid productoId)
        {
            var cliente = await _clienteRepository.GetAsync(clienteId);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            cliente.EntregarPremio(descripcion, productoId);
            await _clienteRepository.UpdateAsync(cliente);
        }


        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
