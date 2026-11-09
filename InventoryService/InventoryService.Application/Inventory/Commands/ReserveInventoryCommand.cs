using InventoryService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Inventory.Commands
{
    public record ReserveInventoryCommand(Guid OrderId, List<ReservationItemDto> Items) : IRequest;
}
