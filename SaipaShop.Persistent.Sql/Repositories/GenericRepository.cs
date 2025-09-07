using SaipaShop.Application.Common;
using SaipaShop.Domain.DomainDto.Pagination;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Repositories;
using SaipaShop.Persistent.Sql.Context;
using SaipaShop.Persistent.Sql.Extensions;
using Microsoft.EntityFrameworkCore;
using SaipaShop.Domain.Entities.Users;

namespace SaipaShop.Persistent.Sql.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(IUnitOfWork uow)
    {
        _dbSet = uow.Set<TEntity>();
    }



    public async Task<PagedResult<TEntity>> GetAllWithFiltersAsync(BaseListFilter<TEntity> listFilter, CancellationToken cancellationToken)
    {
        var query = _dbSet
            .AsNoTracking()
            .AsQueryable();

        query=listFilter.SetFilterQuery(query);

        if (listFilter.HasOrder)
        {
            query = query.OrderByDynamic(listFilter.OrderPropertyName, listFilter.IsAscending);
        }
        return await
            query
               
                .PaginationAsync(listFilter,cancellationToken);
    }

    public async Task<List<TEntity>> GetAllWithFiltersInOnePageAsync(BaseListFilter<TEntity> listFilter, CancellationToken cancellationToken)
    {
        var query = _dbSet
            .AsNoTracking()
            .AsQueryable();

        query=listFilter.SetFilterQuery(query);

        if (listFilter.HasOrder)
        {
            query = query.OrderByDynamic(listFilter.OrderPropertyName, listFilter.IsAscending);
        }
        return await query
            
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<X>> GetAllWithFiltersInDtoAsync<T,X>(BaseListFilter<TEntity> listFilter, CancellationToken cancellationToken) where X:class
    {
        var query = _dbSet
            .AsNoTracking()
            .AsQueryable();

        query=listFilter.SetFilterQuery(query);

        if (listFilter.HasOrder)
        {
            if (!String.IsNullOrEmpty(listFilter.OrderPropertyName))
            {
                query = query.OrderByDynamic(listFilter.OrderPropertyName, listFilter.IsAscending);
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }
        }
     
            
    
        var selectQuery=query
            .SelectDynamic<TEntity, X>();
        
        return await selectQuery
                .PaginationAsync(listFilter,cancellationToken);
    }

 
    
    public async Task<TEntity> GetByIdWithNoTrackingAsync(int id,CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<TEntity> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> GetOneAsync(BaseListFilter<TEntity> filter, CancellationToken cancellationToken)
    {
        var query = _dbSet
            .AsNoTracking()
            .AsQueryable();
        
        query=filter.SetFilterQuery(query);

        return await query
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PagedResult<TEntity>> GetAllAsync(PaginationData paginationData, CancellationToken cancellationToken)
    {

        var query = _dbSet
            .AsNoTracking();
        
        if (paginationData.HasOrder)
        {
            query = query.OrderByDynamic(paginationData.OrderPropertyName, paginationData.IsAscending);

        }
        return await query
            .PaginationAsync(paginationData,cancellationToken);

    }



    public async Task<IEnumerable<TEntity>> GetAllWithTrackingAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync();
    }
    public async Task CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    

    public async Task<bool> DeleteAsync(int id,CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id,cancellationToken);
        if (entity!= null)
        {
            _dbSet.Remove(entity);
            return true;
        }

        return false;
    }

    public async Task CreateRangeAsync(IEnumerable<TEntity> list,CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(list,cancellationToken);
    }
}