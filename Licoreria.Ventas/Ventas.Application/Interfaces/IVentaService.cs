using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Request;

namespace Ventas.Application.Interfaces
{
    public interface IVentaService
    {
        Task RegistrarVenta(DtoRequestVenta requestVenta);
    }
}
