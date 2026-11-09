using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Contracts.Events
{
    public record InventoryReservedEvent(Guid OrderId, DateTime ReservedAtUtc);
}
