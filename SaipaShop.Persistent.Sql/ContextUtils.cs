using System.Linq.Expressions;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Persistent.Sql.Converter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace SaipaShop.Persistent.Sql;

public static class ContextUtils
{
    private const int DefaultStringMaxLength = 100;

    //set global configs for entities and types and configs
    public static ModelConfigurationBuilder AddDefaultConfigs(this ModelConfigurationBuilder modelConfigurationBuilder)
    {
        modelConfigurationBuilder
            .Properties<string>()
            .HaveMaxLength(DefaultStringMaxLength);
        return modelConfigurationBuilder;
    }

    /// <summary>
    /// support the domain property save a model as json in nvarchar(max) field
    /// </summary>
    /// <param name="propertyBuilder"></param>
    /// <typeparam name="TProperty"></typeparam>
    public static void SetAsJsonProperty<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        propertyBuilder
            .HasColumnType("nvarchar(max)")
            .HasConversion(new ValueConverter<TProperty,string>
            (wgr=>JsonConvert.SerializeObject(wgr),
                json=>JsonConvert.DeserializeObject<TProperty>(json)));
    }
    
    /// <summary>
    /// append query filter instead of SetGlobalQuery just replace new ones
    /// </summary>
    /// <param name="entityTypeBuilder"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    public static void AppendQueryFilter<T>(
        this EntityTypeBuilder<T> entityTypeBuilder, Expression<Func<T, bool>> expression)
        where T : class
    {
        var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);

        var expressionFilter = ReplacingExpressionVisitor.Replace(
            expression.Parameters.Single(), parameterType, expression.Body);

        if (entityTypeBuilder.Metadata.GetQueryFilter() != null)
        {
            var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
            var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
            expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
        }

        var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
        entityTypeBuilder.HasQueryFilter(lambdaExpression);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelConfigurationBuilder"></param>
    /// <returns></returns>
    public static ModelConfigurationBuilder AddNewTypeConfig(this ModelConfigurationBuilder modelConfigurationBuilder)
    {
        modelConfigurationBuilder
            .Properties<TimeSpan>()
            .HaveConversion<TimeSpanConverter>();

        modelConfigurationBuilder
            .Properties<int[]>()
            .HaveConversion<IntArrayConverter>();
            
        modelConfigurationBuilder
            .Properties<string[]>()
            .HaveConversion<StringArrayConverter>();
            
        modelConfigurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
            
        modelConfigurationBuilder
            .Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>();
        
        return modelConfigurationBuilder;
    }

    
    //===========================================================
    
    public static IEnumerable<IMutableEntityType> GetEntities(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes();
        return entities;
    }
    
    public static List<Type> GetClrTypes(this ModelBuilder modelBuilder, Type type)
    {
        var entities = GetEntities(modelBuilder);
        var types =entities
            .Select(x => x.ClrType)
            .Where(x=>x.BaseType==type || x.GetInterfaces().Contains(type))
            .ToList();

        return types;
    }

    public static void UpdateModificationDate(ChangeTracker changeTracker,bool considerCreation)
    {
        var entries = changeTracker
            .Entries()
            .Where(e => e.Entity is IDateAuditFields && (
                (considerCreation && e.State == EntityState.Added)
                || e.State == EntityState.Modified));
        foreach (var entityEntry in entries)
        {
                ((IDateAuditFields) entityEntry.Entity).ModificationDate = DateTimeOffset.Now;
        }
    }

    public static void UpdateWhoCreated(ChangeTracker changeTracker,int? userId)
    {
        var entries = changeTracker
            .Entries()
            .Where(e => e.Entity is IWhoAuditFields && 
                e.State == EntityState.Added);
        foreach (var entityEntry in entries)
        {
            ((IWhoAuditFields)entityEntry.Entity).WhoCreated = userId;
        }
    }
    
    public static void UpdateWhoMadeChanges(ChangeTracker changeTracker,int? userId)
    {
        var entries = changeTracker
            .Entries()
            .Where(e => e.Entity is IWhoAuditFields && 
                        e.State == EntityState.Modified);
        foreach (var entityEntry in entries)
        {
            ((IWhoAuditFields)entityEntry.Entity).WhoDidLastModification = userId;
        }
    }
    
  
}