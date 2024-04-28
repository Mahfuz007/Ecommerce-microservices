using Marten.Pagination;

namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category, int? PageNo = 1, int? PageSize = 10) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(IDocumentSession _session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await _session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToPagedListAsync(query.PageNo ?? 1, query.PageSize ?? 10, cancellationToken);
        return new GetProductsByCategoryResult(result);
    }
}