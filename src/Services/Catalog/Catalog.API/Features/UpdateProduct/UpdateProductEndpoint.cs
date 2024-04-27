namespace Catalog.API.Features.UpdateProduct;

public record UpateProductRequest(Guid Id, string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price);
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products", async (UpateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
         .WithName("UpateProduct")
         .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .WithDescription("Upate Product")
         .WithSummary("Upate Product");
    }
}