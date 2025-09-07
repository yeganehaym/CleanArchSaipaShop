using System.Linq.Expressions;
using System.Reflection;
using SaipaShop.Domain.DomainDto.Pagination;
using Microsoft.EntityFrameworkCore;

namespace SaipaShop.Persistent.Sql.Extensions;

public static class QueryExtensions
{
    public static async Task<PagedResult<T>> PaginationAsync<T>(this IQueryable<T> queryable, PaginationData paginationData,CancellationToken cancellationToken) where T:class
    {
        var totalCount =await queryable.CountAsync();
        var results =await queryable.Skip(paginationData.Skip).Take(paginationData.PageSize).ToListAsync(cancellationToken);
        return new PagedResult<T>
        {
            PageSize = paginationData.PageSize,
            PageNumber = paginationData.PageNumber,
            TotalCount = totalCount,
            Results = results
        };
    }
    
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> queryable, string orderByMember,bool isAscending) where T:class
    {
        if (String.IsNullOrEmpty(orderByMember))
            return queryable;
        
        var parameterExpression = Expression.Parameter(typeof(T));
        var memberAccess = Expression.PropertyOrField(parameterExpression, orderByMember);
        var keySelector = Expression.Lambda(memberAccess, parameterExpression);

        var orderBy = Expression.Call(typeof(Queryable),
            isAscending ? "OrderBy" : "OrderByDescending",
            new Type[] { typeof(T), memberAccess.Type },
            queryable.Expression,
            Expression.Quote(keySelector));

        return queryable.Provider.CreateQuery<T>(orderBy);
    }
    
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> queryable, Expression<Func<T,T>> orderByMember,bool isAscending) where T:class
    {
        var parameterExpression = Expression.Parameter(typeof(T));
        var member = (MemberExpression)orderByMember.Body;
        var memberAccess = Expression.PropertyOrField(parameterExpression, member.Member.Name);
        var keySelector = Expression.Lambda(memberAccess, parameterExpression);

        var orderBy = Expression.Call(typeof(Queryable),
            isAscending ? "OrderBy" : "OrderByDescending",
            new Type[] { typeof(T), memberAccess.Type },
            queryable.Expression,
            Expression.Quote(keySelector));

        return queryable.Provider.CreateQuery<T>(orderBy);
    }
    
    public static IQueryable<X> SelectDynamic<T, X>(this IQueryable<T> query) where X : class
    {
        var properties = typeof(X).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var parameter = Expression.Parameter(typeof(T), "e");
        var bindings = properties.Select(p => Expression.Bind(p, Expression.Property(parameter, p.Name)));
        var selectExpression = Expression.Lambda(Expression.MemberInit(Expression.New(typeof(X)), bindings), parameter);
        return query.Provider.CreateQuery<X>(Expression.Call(typeof(Queryable), "Select", new Type[] { typeof(T), typeof(X) }, query.Expression, Expression.Quote(selectExpression)));
    }

  
}