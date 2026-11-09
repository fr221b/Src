using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contracts;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    internal sealed class OrderRepository(OrderDbContext db) : IOrderRepository
    {
        public async Task AddAsync(Order order, CancellationToken ct) =>
        await db.Orders.AddAsync(order, ct);

        public Task<Order?> GetByIdAsync(Guid id, CancellationToken ct) =>
            db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);

        public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
    }
}
