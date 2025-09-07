namespace SaipaShop.Domain.DomainDto.Pagination;

public class PagedResult<T> where T : class
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalCount { get; set; }
    public int PageCount =>(int) Math.Ceiling((decimal)TotalCount / PageSize);
    public IEnumerable<T> Results { get; set; }
}