using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Fail
{
    public record FailOrderCommand(Guid OrderId, string Reason) : IRequest;
}
