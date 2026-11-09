using MediatR;
using OrderService.Application.Contracts;
using OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Queries
{
    public class GetOrderByIdQueryHandler(IOrderRepository repository)
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        #region [- Handle -]
        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (order is null) return null;

            return new OrderDto(
                order.Id,
                order.CustomerId,
                order.Status.ToString(),
                order.FailureReason,
                order.TotalAmount,
                order.CreatedAtUtc,
                order.UpdatedAtUtc,
                order.Items.Select(i => new OrderDetailDto(i.ProductId, i.Quantity, i.UnitPrice)).ToList());
        } 
        #endregion
    }
}
