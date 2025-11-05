using Clientes.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetAsync(Guid id);
        Task<ICollection<Cliente>> ListAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
    }
}
