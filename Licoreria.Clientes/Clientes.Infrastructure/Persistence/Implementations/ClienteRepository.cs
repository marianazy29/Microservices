using Clientes.Domain.Aggregates;
using Clientes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.Persistence.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task Add(Cliente cliente)
        {
            await _context.Set<Cliente>().AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Cliente cliente)
        {          
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var cliente = await _context.Set<Cliente>().FindAsync(id);
            if (cliente != null)
            {
                _context.Set<Cliente>().Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente?> GetById(Guid id)
        {
            return await _context.Set<Cliente>()
                .Include(c => c.Premios)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Cliente>> List()
        {
            var a = await _context.Set<Cliente>()
                .ToListAsync();
            return await _context.Set<Cliente>()
                .ToListAsync();
        }

       
    }
}
