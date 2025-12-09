
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);
public class CreateOrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var ordersGroup = app.MapGroup("/api/v1/orders");
        ordersGroup.MapPost("/", async (CreateOrderRequest request, ISender sender) =>
        {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/api/v1/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new order")
        .WithDescription("Endpoint to create a new order for a customer");
    }
}