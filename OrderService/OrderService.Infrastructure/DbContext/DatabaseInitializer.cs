using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.DbContext
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeOrderDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderDbContext>>();

            const int maxAttempts = 10;
            for (var attempt = 1; ; attempt++)
            {
                try
                {
                    await db.Database.EnsureCreatedAsync();
                    logger.LogInformation("Order database is ready.");
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
