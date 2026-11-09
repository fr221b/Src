using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Fail
{
    public class FailOrderCommandHandler(
    IOrderRepository repository,
    ILogger<FailOrderCommandHandler> logger) : IRequestHandler<FailOrderCommand>
    {
        #region [- Handle -]
        public async Task Handle(FailOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.GetByIdAsync(request.OrderId, cancellationToken)
                    ?? throw new Exception("Order Not Found!");

            if (!order.Fail(request.Reason))
            {
                logger.LogWarning("Order {OrderId} is already {Status}; duplicate message ignored.", order.Id, order.Status);
                return;
            }

            await repository.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Order {OrderId} failed: {Reason}", order.Id, request.Reason);
        } 
        #endregion
    }
}
