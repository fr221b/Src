using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Dtos
{
    public record ProductDto(Guid Id, string Name, int AvailableQuantity);
}
