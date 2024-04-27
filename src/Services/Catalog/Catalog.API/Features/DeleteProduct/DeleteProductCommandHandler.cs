namespace Catalog.API.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
public class DeleteProductCommandHandler(IDocumentSession _session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _session.Delete<Product>(command.Id);
        await _session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}