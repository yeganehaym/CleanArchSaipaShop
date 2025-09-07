using System.Reflection;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Persistent.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace SaipaShop.Persistent.Sql.QueryFilters;

public static class GlobalRemoveQueryFilter
{
    public static void AddGlobalRemoveQueryFilter<TContext>(this ModelBuilder modelBuilder,TContext context,Type removeBaseClass) where TContext:DbContext
    {
        var types = modelBuilder.GetClrTypes(removeBaseClass);
        foreach (var type in types)
        {
            var method = SetGlobalRemoveQueryMethod.MakeGenericMethod(type);
            method.Invoke(context, new object[] { modelBuilder });
        }
    }
    
    public static void SetGlobalRemoveQuery<T>(this ModelBuilder builder) where T : class, IBaseRemovedEntity
    {
        builder.Entity<T>().AppendQueryFilter(e => e.IsRemoved==false);
    }
        
    static readonly MethodInfo SetGlobalRemoveQueryMethod = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalRemoveQuery");


}