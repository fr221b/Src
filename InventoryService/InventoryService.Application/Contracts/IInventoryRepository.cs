using InventoryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Contracts
{
    public interface IInventoryRepository
    {
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct);
        Task<List<Product>> GetAllProductsAsync(CancellationToken ct);                
        Task SaveChangesAsync(CancellationToken ct);
    }
}
