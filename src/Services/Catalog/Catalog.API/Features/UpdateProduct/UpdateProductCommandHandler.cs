namespace Catalog.API.Features.UpdateProduct;

public record UpateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler(IDocumentSession _session) : ICommandHandler<UpateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            ImageFileUrl = command.ImageFileUrl,
            Price = command.Price
        };

        _session.Store<Product>(product);
        await _session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
