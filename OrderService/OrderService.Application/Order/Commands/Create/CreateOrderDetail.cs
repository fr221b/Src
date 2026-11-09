using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Order.Commands.Create
{
    public record CreateOrderDetail(Guid ProductId, int Quantity, decimal UnitPrice);
}
