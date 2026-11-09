using OrderProcessing.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Contracts.Events
{
    public record OrderCreatedEvent(
    Guid OrderId,
    Guid CustomerId,
    IReadOnlyList<OrderDetailMessage> Items,
    DateTime CreatedAtUtc);
}
