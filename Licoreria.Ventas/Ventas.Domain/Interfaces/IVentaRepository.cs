using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Domain.Aggregates;

namespace Ventas.Domain.Interfaces
{
    public interface IVentaRepository
    {
        Task<Venta?> GetById(Guid id);
        Task<ICollection<Venta>> List();
        Task<Guid> Add(Venta venta);
        Task Update(Venta venta);
        Task Delete(Guid id);
    }
}
