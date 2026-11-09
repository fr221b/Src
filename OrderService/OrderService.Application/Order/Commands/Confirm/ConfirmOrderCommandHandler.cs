using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Confirm
{
    public class ConfirmOrderCommandHandler(
    IOrderRepository repository,
    ILogger<ConfirmOrderCommandHandler> logger) : IRequestHandler<ConfirmOrderCommand>
    {
        #region [- Handle -]
        public async Task Handle(ConfirmOrderCommand request, CancellationToken ct)
        {
            var order = await repository.GetByIdAsync(request.OrderId, ct)
                    ?? throw new Exception("Order is null!");

            if (!order.Confirm())
            {
                logger.LogWarning("Order {OrderId} is already {Status}; duplicate message ignored.", order.Id, order.Status);
                return;
            }

            await repository.SaveChangesAsync(ct);
            logger.LogInformation("Order {OrderId} confirmed.", order.Id);
        }

        
    } 
        #endregion
    }
