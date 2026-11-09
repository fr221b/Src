using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OrderService.Application;
using OrderService.Application.Contracts;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.DbContext;
using OrderService.Infrastructure.Producers;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration config)
        {
           
            services.AddDbContext<OrderDbContext>(o =>
                o.UseNpgsql(config.GetConnectionString("OrdersDb")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEventProducer, EventPublisher>();

            services.AddMassTransit(x =>
            {
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("order-service", false));

                x.AddConsumer<InventoryReservedConsumer>();
                x.AddConsumer<InventoryFailedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["RabbitMq:Host"] ?? "localhost", config["RabbitMq:VirtualHost"] ?? "/", h =>
                    {
                        h.Username(config["RabbitMq:Username"] ?? "guest");
                        h.Password(config["RabbitMq:Password"] ?? "guest");
                    });

                    cfg.UseMessageRetry(r => r.Intervals(100, 500, 1000, 3000));

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
