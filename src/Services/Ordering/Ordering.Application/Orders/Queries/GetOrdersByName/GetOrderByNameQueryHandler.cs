using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrderByNameQueryHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
                                    .Include(o => o.OrderItems)
                                    .AsNoTracking()
                                    .Where(o => o.Name.Value.Contains(query.name))
                                    .OrderBy(o => o.Name.Value)
                                    .ToListAsync();

        return new GetOrderByNameResult(order.OrderToOrderDtoList());
    }
}