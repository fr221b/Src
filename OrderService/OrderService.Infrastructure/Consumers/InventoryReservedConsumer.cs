using MassTransit;
using MediatR;
using OrderProcessing.Contracts.Events;
using OrderService.Application.Order.Commands.Confirm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Consumers
{
    public class InventoryReservedConsumer(ISender sender) : IConsumer<InventoryReservedEvent>
    {
        public Task Consume(ConsumeContext<InventoryReservedEvent> context) =>
           sender.Send(new ConfirmOrderCommand(context.Message.OrderId), context.CancellationToken);
    }
}
