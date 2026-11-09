using InventoryService.Application.Dtos;
using InventoryService.Application.Inventory.Commands;
using MassTransit;
using MediatR;
using OrderProcessing.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Consumers
{
    public class OrderCreatedConsumer(ISender sender) : IConsumer<OrderCreatedEvent>
    {
        #region [- Consume -]
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            var command = new ReserveInventoryCommand(
                message.OrderId,
                message.Items.Select(i => new ReservationItemDto(i.ProductId, i.Quantity)).ToList());

            return sender.Send(command, context.CancellationToken);
        } 
        #endregion
    }
}
