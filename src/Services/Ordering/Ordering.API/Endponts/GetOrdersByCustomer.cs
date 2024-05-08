using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endponts;

//public record GetOrdersByCustomerRequest();
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customers/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
            //Issue: Mapper provide null value. will need to fix it and return response.
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(result);
        })
          .WithName("GetOrdersByCustomer")
          .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Get Orders By Customer")
          .WithSummary("Get OrdersBy Customer");
    }
}
