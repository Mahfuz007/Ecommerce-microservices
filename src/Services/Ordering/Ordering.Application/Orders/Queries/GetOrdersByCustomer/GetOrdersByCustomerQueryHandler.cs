using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler (IApplicationDbContext _dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .AsNoTracking()
                                        .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                                        .OrderBy(o => o.Name.Value)
                                        .ToListAsync();

        return new GetOrdersByCustomerResult(orders.OrderToOrderDtoList());
    }
}
