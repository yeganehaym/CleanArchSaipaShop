namespace SaipaShop.Domain.DomainDto.Pagination;

public class PaginationData
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public int Skip => (PageNumber - 1) * PageSize;

    public bool HasOrder => !String.IsNullOrEmpty(OrderPropertyName);
    
    public string OrderPropertyName { get; set; }
    public bool IsAscending { get; set; }
    
}