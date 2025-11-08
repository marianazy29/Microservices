using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Request;
using Ventas.Application.Response;

namespace Ventas.Application.Interfaces
{
    public interface IVentaService
    {
        Task<string> RegistrarVenta(DtoRequestVenta requestVenta);
        Task<ICollection<DtoResponseCliente>> ObtenerClientes();
    }
}
