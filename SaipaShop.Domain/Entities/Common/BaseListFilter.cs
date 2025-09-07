using SaipaShop.Domain.DomainDto.Pagination;

namespace SaipaShop.Domain.Entities.Common;

public abstract class BaseListFilter<T>:
    
    
    PaginationData where T:class
{
    public string? SearchWord { get; set; }
    protected bool IsSearchable => !String.IsNullOrEmpty(SearchWord);
    public abstract IQueryable<T> SetFilterQuery(IQueryable<T> query);

    
}