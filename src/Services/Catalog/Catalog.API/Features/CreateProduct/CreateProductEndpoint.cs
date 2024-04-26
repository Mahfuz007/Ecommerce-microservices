using Carter;
using MediatR;
using Mapster;

namespace Catalog.API.Features.CreateProduct;

public record CreateProductRequest(string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"products/{response.Id}", response);
        })
         .WithName("CreateProduct")
         .Produces<CreateProductResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .WithDescription("Create Product")
         .WithSummary("Create Product");
    }
}