using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Domain.Aggregates;
using Ventas.Domain.Interfaces;
using Ventas.Infrastructure.Persistence;

namespace Ventas.Infrastructure.Implementations
{
    public class VentaRepository : IVentaRepository
    {
        private readonly VentaDbContext _context;

        public VentaRepository(VentaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Venta venta)
        {
            await _context.Set<Venta>().AddAsync(venta);
            await _context.SaveChangesAsync();

            return venta.Id;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Venta?> GetById(Guid id)
        {
            return await _context.Set<Venta>()
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<ICollection<Venta>> List()
        {
            return await _context.Set<Venta>()
                .Include(v => v.Detalles)
                .ToListAsync();
        }

        public Task Update(Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}
