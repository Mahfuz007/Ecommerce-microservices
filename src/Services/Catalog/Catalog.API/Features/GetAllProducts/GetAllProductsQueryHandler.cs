using Marten.Pagination;

namespace Catalog.API.Features.GetAllProducts;

public record GetAllProductsQuery(int? PageNo = 1, int? PageSize = 10) : IQuery<GetAllProductsResult>;
public record GetAllProductsResult(IEnumerable<Product> Products);
internal class GetAllProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>().ToPagedListAsync(query.PageNo ?? 1, query.PageSize ?? 10,cancellationToken);
        return new GetAllProductsResult(result);
    }
}
