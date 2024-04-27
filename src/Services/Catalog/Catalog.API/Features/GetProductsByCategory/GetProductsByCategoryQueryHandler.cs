using Catalog.API.Models;
using CommonBlocks.CQRS;
using Marten;

namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(IDocumentSession _session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await _session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync();
        return new GetProductsByCategoryResult(result);
    }
}