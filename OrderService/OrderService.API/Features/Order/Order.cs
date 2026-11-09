using MediatR;
using OrderService.API.Models.Request;
using OrderService.Application.Dtos;
using OrderService.Application.Order.Commands.Create;
using OrderService.Application.Order.Queries;

namespace OrderService.API.Features.Order
{
    public static class Order
    {
        #region [- MapOrderEndpoints -]
        public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/orders").WithTags("Orders");

            group.MapPost("/", CreateOrder)
                .WithName("CreateOrder")
                .WithSummary("OrderRegisteration")
                .Produces(StatusCodes.Status202Accepted)
                .ProducesValidationProblem()
                .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapGet("/{id:guid}", GetOrderById)
                .WithName("GetOrderById")
                .WithSummary("OrderReport")
                .Produces<OrderDto>()
                .Produces(StatusCodes.Status404NotFound);

            return app;
        }
        #endregion

        #region [- CreateOrder -]
        private static async Task<IResult> CreateOrder(
        OrderRequestModel request, ISender sender, CancellationToken ct)
        {
            var command = new CreateOrderCommand(
                request.CustomerId,
                request.Items.Select(i => new CreateOrderDetail(i.ProductId, i.Quantity, i.UnitPrice)).ToList());

            var orderId = await sender.Send(command, ct);

            return Results.AcceptedAtRoute("GetOrderById", new { id = orderId }, new { orderId, status = "Pending" });
        }
        #endregion

        #region [- GetOrderById -]
        private static async Task<IResult> GetOrderById(Guid id, ISender sender, CancellationToken ct)
        {
            var order = await sender.Send(new GetOrderByIdQuery(id), ct);
            return order is null ? Results.NotFound() : Results.Ok(order);
        } 
        #endregion
    }
}
