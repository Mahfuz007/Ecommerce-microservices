
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endponts;

//public record GetOrdersByNameRequest();
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByNameQuery(name));
            //Issue: Mapper provide null value. will need to fix it and return response.
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(result);
        })
          .WithName("GetOrdersByName")
          .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Get Orders By Name")
          .WithSummary("Get Orders By Name");
    }
}
