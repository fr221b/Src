using InventoryService.Application.Contracts;
using InventoryService.Infrastructure.Consumers;
using InventoryService.Infrastructure.DbContext;
using InventoryService.Infrastructure.Producers;
using InventoryService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure
{
    public static class DependencyResolver
    {
        #region [- AddInfrastructure -]
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<InventoryDbContext>(o =>
                o.UseNpgsql(config.GetConnectionString("InventoryDb")));
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IEventProducer, InventoryEventPublisher>();

            services.AddMassTransit(x =>
            {
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("inventory-service", false));
                x.AddConsumer<OrderCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["RabbitMq:Host"] ?? "localhost", config["RabbitMq:VirtualHost"] ?? "/", h =>
                    {
                        h.Username(config["RabbitMq:Username"] ?? "guest");
                        h.Password(config["RabbitMq:Password"] ?? "guest");
                    });

                    cfg.UseMessageRetry(r => r.Intervals(100, 300, 500, 1000, 3000));

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        } 
        #endregion
    }
}
