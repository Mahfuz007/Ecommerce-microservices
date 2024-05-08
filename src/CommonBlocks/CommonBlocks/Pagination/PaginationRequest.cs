namespace CommonBlocks.Pagination;

public record PaginationRequest(int PageNumber = 0, int PageSize = 10)
{
}