using CommonBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endponts;

// record GetOrderRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginationResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));
            //Issue: Mapper provide null value. will need to fix it and return response.
            var reponse = result.Adapt<GetOrdersResponse>();

            return Results.Ok(result);
        })
          .WithName("GetOrders")
          .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithDescription("Get Orders")
          .WithSummary("Get Orders");
    }
}
