using System.Reflection;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Persistent.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace SaipaShop.Persistent.Sql.QueryFilters;

public static class GlobalTenantQueryFilter
{
    public static void AddGlobalTenantQueryFilter<TContext>(this ModelBuilder modelBuilder,TContext context,Type tenantBaseClass) where TContext:DbContext
    {
        var types = modelBuilder.GetClrTypes(tenantBaseClass);
        foreach (var type in types)
        {
            var method = SetGlobalTenantQueryMethod.MakeGenericMethod(type);
            method.Invoke(context, new object[] { modelBuilder });
        }
    }
    
    public static void SetGlobalTenantQuery<T>(this ModelBuilder builder,Func<int> getTenantIdFunction) where T : class, IBaseTenantIdEntity
    {
        builder.Entity<T>().AppendQueryFilter(e => e.TenantId==getTenantIdFunction());
    }
        
    static readonly MethodInfo SetGlobalTenantQueryMethod = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalTenantQuery");


}