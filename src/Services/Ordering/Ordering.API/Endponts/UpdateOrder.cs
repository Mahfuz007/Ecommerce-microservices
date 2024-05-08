using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endponts;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async(UpdateOrderRequest request, ISender sender) =>
        {
            //var command = request.Adapt<CreateOrderRequest>();
            var result = await sender.Send(new UpdateOrderCommand(request.Order));
            //Issue: Mapper provide null value. will need to fix it and return response.
            //var response = result.Adapt<CreateOrderResponse>();
            return Results.Ok(result);
        })
          .WithName("UpdateOrder")
          .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Update Order")
          .WithSummary("Update Order");
    }
}