using InventoryService.Application.Contracts;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Repositories
{
    public sealed class InventoryRepository(InventoryDbContext db) : IInventoryRepository
    {
        public Task<List<Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct)
        {
            var idList = ids.ToList();
            return db.Products.Where(p => idList.Contains(p.Id)).ToListAsync(ct);
        }

        public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);

        public async Task<List<Product>> GetAllProductsAsync(CancellationToken ct)
        {
            return await db.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync(ct);
        }
    }
}
