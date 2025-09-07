using System.Reflection;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Persistent.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace SaipaShop.Persistent.Sql.QueryFilters;

public static class GlobalTemporaryRemoveQueryFilter
{
    public static void AddGlobalTemporaryRemoveQueryFilter<TContext>(this ModelBuilder modelBuilder,TContext context,Type temporaryRemoveBaseClass) where TContext:DbContext
    {
        var types = modelBuilder.GetClrTypes(temporaryRemoveBaseClass);
        foreach (var type in types)
        {
            var method = SetGlobalTempoRemoveQueryMethod.MakeGenericMethod(type);
            method.Invoke(context, new object[] { modelBuilder });
        }
    }
    
    public static void SetGlobalTempoRemoveQuery<T>(this ModelBuilder builder) where T : class, IBaseTemporaryDeleteEntity
    {
        builder.Entity<T>().AppendQueryFilter(e => !e.TemporaryDelete.HasValue || e.TemporaryDelete.Value < DateTimeOffset.Now);
    }
        
    static readonly MethodInfo SetGlobalTempoRemoveQueryMethod = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalTempoRemoveQuery");


}