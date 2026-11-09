using MassTransit;
using MediatR;
using OrderProcessing.Contracts.Events;
using OrderService.Application.Order.Commands.Fail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Consumers
{
    public class InventoryFailedConsumer(ISender sender) : IConsumer<InventoryFailedEvent>
    {
        public Task Consume(ConsumeContext<InventoryFailedEvent> context) =>
        sender.Send(new FailOrderCommand(context.Message.OrderId, context.Message.Reason), context.CancellationToken);
    }
}
