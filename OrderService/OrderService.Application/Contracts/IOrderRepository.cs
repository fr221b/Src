using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Contracts
{
    public interface IOrderRepository
    {
        Task AddAsync(Domain.Entities.Order order, CancellationToken ct);
        Task<Domain.Entities.Order?> GetByIdAsync(Guid id, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
