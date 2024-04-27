namespace Catalog.API.Features.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price)
        : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler (IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            ImageFileUrl = command.ImageFileUrl,
            Price = command.Price
        };

        session.Store<Product>(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
