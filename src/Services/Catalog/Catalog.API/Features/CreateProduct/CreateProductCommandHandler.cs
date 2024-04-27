namespace Catalog.API.Features.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price)
        : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Name field is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description field is required");
        RuleFor(x => x.ImageFileUrl).NotEmpty().WithMessage("ImageFileUrl field is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Field is required");
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage("Price is required and should greate than 0");
    }
}
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
