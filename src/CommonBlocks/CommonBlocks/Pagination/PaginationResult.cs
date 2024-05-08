namespace CommonBlocks.Pagination;

public class PaginationResult<IEntity>(int pageNumber, int pageSize, long totalCount, IEnumerable<IEntity> data)
    where IEntity : class
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public long TotalCount { get; } = totalCount;
    public IEnumerable<IEntity> Data { get; } = data;
}
