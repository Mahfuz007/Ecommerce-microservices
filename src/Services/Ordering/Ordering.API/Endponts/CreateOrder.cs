using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endponts;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(string OrderName);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            //var command = request.Adapt<CreateOrderRequest>();
            var result = await sender.Send(new CreateOrderCommand(request.Order));
            //Issue: Mapper provide null value. will need to fix it and return response.
            //var response = result.Adapt<CreateOrderResponse>();
            return Results.Created("orders/created", result);
        })
          .WithName("CreatOrder")
          .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Creat Order")
          .WithSummary("Creat Order");
    }
}
