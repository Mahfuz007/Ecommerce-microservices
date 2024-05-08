namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand (Guid Id) : ICommand<DeleteOrderResult> { }

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand> { 
    public DeleteOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
