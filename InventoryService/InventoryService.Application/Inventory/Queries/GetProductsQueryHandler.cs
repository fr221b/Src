using InventoryService.Application.Contracts;
using InventoryService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Inventory.Queries
{
    public class GetProductsQueryHandler(IInventoryRepository repository)
    : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        #region [- Handle -]
        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetAllProductsAsync(cancellationToken);
            return products.Select(p => new ProductDto(p.Id, p.Name, p.AvailableQuantity)).ToList();
        } 
        #endregion
    }
}
