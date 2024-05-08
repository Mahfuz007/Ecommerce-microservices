using CommonBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageNo = query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await _dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .OrderBy(o => o.Name.Value)
                                        .Skip(pageSize * pageNo)
                                        .Take(pageSize)
                                        .ToListAsync(cancellationToken);
        return new GetOrdersResult(new PaginationResult<Order>(pageNo, pageSize, totalCount, orders));
    }
}