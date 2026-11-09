using InventoryService.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Inventory.Commands
{
    public class ReserveInventoryCommandHandler(
    IInventoryRepository repository,
    IEventProducer publisher,
    ILogger<ReserveInventoryCommandHandler> logger) : IRequestHandler<ReserveInventoryCommand>
    {
        #region [- Handle -]
        public async Task Handle(ReserveInventoryCommand request, CancellationToken cancellationToken)
        {
            var requested = request.Items
                                   .GroupBy(i => i.ProductId)
                                   .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));

            var products = (await repository.GetProductsByIdsAsync(requested.Keys, cancellationToken))
                .ToDictionary(p => p.Id);

            string? failureReason = null;
            foreach (var (productId, quantity) in requested)
            {
                if (!products.TryGetValue(productId, out var product))
                {
                    failureReason = $"Product '{productId}' was not found.";
                    break;
                }

                if (!product.HasStock(quantity))
                {
                    failureReason = $"Insufficient stock for '{product.Name}'. Requested {quantity}, available {product.AvailableQuantity}.";
                    break;
                }
            }

            if (failureReason is not null)
            {
                logger.LogWarning("Reservation failed for order {OrderId}: {Reason}", request.OrderId, failureReason);
                await publisher.PublishAsync(
                    new InventoryFailedEvent(request.OrderId, failureReason, DateTime.UtcNow), cancellationToken);
                return;
            }


            foreach (var (productId, quantity) in requested)
                products[productId].Reserve(quantity);

            await repository.SaveChangesAsync(cancellationToken);

            await publisher.PublishAsync(
                new InventoryReservedEvent(request.OrderId, DateTime.UtcNow), cancellationToken);
        } 
        #endregion
    }
}
