using InventoryService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Inventory.Queries
{
    public class GetProductsQuery : IRequest<List<ProductDto>>;
}
