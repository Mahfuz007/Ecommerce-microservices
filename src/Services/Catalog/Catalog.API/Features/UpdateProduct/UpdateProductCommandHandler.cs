namespace Catalog.API.Features.UpdateProduct;

public record UpateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFileUrl, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Name field is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description field is required");
        RuleFor(x => x.ImageFileUrl).NotEmpty().WithMessage("ImageFileUrl field is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Field is required");
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage("Price is required and should greate than 0");
    }
}

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
