﻿namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrderByNameQuery(string name) : IQuery<GetOrderByNameResult>
{
}

public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);
