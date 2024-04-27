using Carter;
using Catalog.API.Models;
using CommonBlocks.CQRS;
using Mapster;
using MediatR;

namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryRequest(string Category) : IQuery<GetProductsByCategoryResponse>;
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var resonse = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(resonse);
        })
          .WithName("GetProductsByCategory")
          .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithDescription("Get Products by Category")
          .WithSummary("Get Products by Category");
    }
}
