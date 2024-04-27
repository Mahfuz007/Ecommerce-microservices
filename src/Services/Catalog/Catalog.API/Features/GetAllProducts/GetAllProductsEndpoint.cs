namespace Catalog.API.Features.GetAllProducts;

//public record GetAllProductsRequest()
public record GetAllProductResponse(List<Product> Products);
public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllProductsQuery());
            var response = result.Adapt(result.Products);
            return response;
        })
          .WithName("GetAllProducts")
         .Produces<GetAllProductResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .WithDescription("Get All Products")
         .WithSummary("Get All Products");
    }
}
