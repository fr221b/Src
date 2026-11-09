using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Contracts.Events;
using OrderProcessing.Contracts.Messages;
using OrderService.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Create
{
    public class CreateOrderCommandHandler(
    IOrderRepository repository,
    IEventProducer publisher,
    ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, Guid>
    {
        #region [- Handle -]
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Domain.Entities.Order.Create(
            request.CustomerId,
            request.Items.Select(i => (i.ProductId, i.Quantity, i.UnitPrice)));

            await repository.AddAsync(order, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            await publisher.PublishAsync(new OrderCreatedEvent(
                order.Id,
                order.CustomerId,
                order.Items.Select(i => new OrderDetailMessage(i.ProductId, i.Quantity)).ToList(),
                order.CreatedAtUtc), cancellationToken);

            logger.LogInformation("Order {OrderId} created (Pending) and OrderCreatedEvent published.", order.Id);
            return order.Id;
        } 
        #endregion
    }
}
