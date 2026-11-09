using InventoryService.Application.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Producers
{
    public sealed class InventoryEventPublisher(IPublishEndpoint publishEndpoint) : IEventProducer
    {
        public Task PublishAsync<T>(T message, CancellationToken ct) where T : class =>
         publishEndpoint.Publish(message, ct);      
    }
}
