using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Create
{
    public record CreateOrderCommand(Guid CustomerId, IReadOnlyList<CreateOrderDetail> Items) : IRequest<Guid>;
}
