using MediatR;
using OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Queries
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}
