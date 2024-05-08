using CommonBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>
{
}

public record GetOrdersResult(PaginationResult<Order> PaginationResult);