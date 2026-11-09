using MassTransit;
using OrderService.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Producers
{
    public sealed class EventPublisher(IPublishEndpoint publishEndpoint) : IEventProducer
    {
        public async Task PublishAsync<T>(T message, CancellationToken ct) where T : class =>
          await publishEndpoint.Publish(message, ct);
    }
}
