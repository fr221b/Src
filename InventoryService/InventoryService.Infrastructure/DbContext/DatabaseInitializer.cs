using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.DbContext
{
    public static class DatabaseInitializer
    {
        public static readonly Guid LaptopId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid MouseId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid KeyboardId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        public static async Task InitializeInventoryDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<InventoryDbContext>>();

            const int maxAttempts = 10;
            for (var attempt = 1; ; attempt++)
            {
                try
                {
                    await db.Database.EnsureCreatedAsync();

                    if (!await db.Products.AnyAsync())
                    {
                        db.Products.AddRange(
                            Product.Create(LaptopId, "Laptop", 10),
                            Product.Create(MouseId, "Mouse", 50),
                            Product.Create(KeyboardId, "Keyboard", 5));
                        await db.SaveChangesAsync();
                        logger.LogInformation("Seeded sample products.");
                    }

                    logger.LogInformation("Inventory database is ready.");
                    return;
                }
                catch (Exception ex) when (attempt < maxAttempts)
                {
                    logger.LogWarning("Database not ready (attempt {Attempt}/{Max}): {Message}", attempt, maxAttempts, ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
        }
    }
}
