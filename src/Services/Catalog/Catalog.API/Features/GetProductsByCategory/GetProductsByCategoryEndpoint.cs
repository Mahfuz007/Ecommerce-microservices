namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryRequest(int? PageNo = 1, int? PageSize = 10) : IQuery<GetProductsByCategoryResponse>;
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", async (string category, [AsParameters] GetProductsByCategoryRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category, request.PageNo, request.PageSize));
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
