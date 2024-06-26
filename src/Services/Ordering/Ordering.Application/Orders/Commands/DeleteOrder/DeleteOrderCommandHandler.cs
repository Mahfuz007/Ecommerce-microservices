﻿namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext _dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FindAsync([OrderId.Of(command.Id)], cancellationToken: cancellationToken);
        if(order is null)
        {
            throw new OrderNotFoundException(command.Id.ToString());
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
