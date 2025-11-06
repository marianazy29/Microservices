using Clientes.Application.Request;
using Clientes.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ICollection<DtoResponseClienteLista>> List();
        Task<DtoResponseCliente?> GetCliente(Guid id);
        Task CrearCliente(DtoRequestCliente request);
        Task SumarPuntos(Guid clienteId, decimal puntos);
        Task EntregarPremio(Guid clienteId, string descripcion, Guid productoId);
        Task Delete(Guid id);
       
    }
}
