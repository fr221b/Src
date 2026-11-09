using InventoryService.Application.Dtos;
using InventoryService.Application.Inventory.Queries;
using MediatR;

namespace InventoryService.API.Features.Product
{
    public static class Product
    {
        #region [- MapProductEndpoints -]
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products").WithTags("Products");

            group.MapGet("/", GetProducts)
                .WithName("GetProducts")
                .WithSummary("Product Report")
                .Produces<IReadOnlyList<ProductDto>>();

            return app;
        } 
        #endregion

        private static async Task<IResult> GetProducts(ISender sender, CancellationToken ct) =>
            Results.Ok(await sender.Send(new GetProductsQuery(), ct));
    }
}
