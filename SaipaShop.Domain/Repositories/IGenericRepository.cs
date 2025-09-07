using SaipaShop.Domain.DomainDto.Pagination;
using SaipaShop.Domain.Entities.Common;

namespace SaipaShop.Domain.Repositories;


public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// find by id and naturally has tracking system
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(int id,CancellationToken cancellationToken);
    Task<TEntity> GetOneAsync(BaseListFilter<TEntity> filter,CancellationToken cancellationToken);
    
    
    /// <summary>
    /// get all records no tracking , useful for a few records no need pagination, for more records and pagination pass PaginationData
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    
    
    /// <summary>
    /// get all data with pagination and applying dynamic sort , no tracking
    /// </summary>
    /// <param name="paginationData"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedResult<TEntity>> GetAllAsync(PaginationData paginationData, CancellationToken cancellationToken);
    Task<PagedResult<TEntity>> GetAllWithFiltersAsync(BaseListFilter<TEntity> listFilter, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllWithFiltersInOnePageAsync(BaseListFilter<TEntity> listFilter, CancellationToken cancellationToken);

    Task<PagedResult<X>> GetAllWithFiltersInDtoAsync<T, X>(BaseListFilter<TEntity> listFilter,
        CancellationToken cancellationToken) where X : class;
    
    
    /// <summary>
    /// get by id but no tracking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdWithNoTrackingAsync(int id,CancellationToken cancellationToken);
    
    
    /// <summary>
    /// get list of data by tracking, for few records is effecient
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllWithTrackingAsync(CancellationToken cancellationToken);
    Task CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id,CancellationToken cancellationToken);
    Task CreateRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken);

}