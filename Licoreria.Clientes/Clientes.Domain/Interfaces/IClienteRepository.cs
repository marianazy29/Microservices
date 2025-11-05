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
        Task<Cliente?> GetById(Guid id);
        Task<ICollection<Cliente>> List();
        Task Add(Cliente cliente);
        Task Update(Cliente cliente);
        Task Delete(Guid id);
    }
}
