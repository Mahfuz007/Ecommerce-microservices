﻿namespace Catalog.API.Features.GetProductById;

//public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
         .WithName("GetProductById")
         .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .WithDescription("Get A Product By Id")
         .WithSummary("Get A Product By Id");
    }
}
