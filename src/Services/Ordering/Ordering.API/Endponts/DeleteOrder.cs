
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endponts;

//public record DeleteOrderRequest();
public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(id));
            var respnse = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(respnse);
        })
          .WithName("DeleteOrder")
          .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Delete Order")
          .WithSummary("Delete Order");
    }
}
