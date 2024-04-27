namespace Catalog.API.Features.GetAllProducts;

public record GetAllProductsQuery() : IQuery<GetAllProductsResult>;
public record GetAllProductsResult(List<Product> Products);
internal class GetAllProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetAllProductsResult((List<Product>)result);
    }
}
