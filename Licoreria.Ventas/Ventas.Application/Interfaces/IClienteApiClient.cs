using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Response;

namespace Ventas.Application.Interfaces
{
    public interface IClienteApiClient
    {
        Task<List<DtoResponseCliente>> ListarClientes();
    }
}
