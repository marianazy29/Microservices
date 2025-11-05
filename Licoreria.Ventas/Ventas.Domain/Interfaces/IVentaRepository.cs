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
        Task<Venta?> GetAsync(Guid id);
        Task<IEnumerable<Venta>> ListAsync();
        Task AddAsync(Venta venta);
        Task UpdateAsync(Venta venta);
        Task DeleteAsync(Guid id);
    }
}
