namespace Catalog.API.Features.GetAllProducts;

public record GetAllProductsRequest(int? PageNo = 1, int? PageSize = 10);
public record GetAllProductResponse(IEnumerable<Product> Products);
public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters]GetAllProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetAllProductsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetAllProductResponse>();
            return response;
        })
          .WithName("GetAllProducts")
         .Produces<GetAllProductResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .WithDescription("Get All Products")
         .WithSummary("Get All Products");
    }
}
