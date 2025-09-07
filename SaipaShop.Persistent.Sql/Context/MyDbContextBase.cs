using System.Reflection;
using SaipaShop.Application.Common;
using SaipaShop.Persistent.Sql.Configs.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SaipaShop.Persistent.Sql.Context;

/// <summary>
/// a middle class between DbContext and ApplicationDbContext to put common configs here to to keep ApplicationDbContext small and soft to eyes
/// </summary>
public class MyDbContextBase:DbContext,IUnitOfWork
{
    public MyDbContextBase(DbContextOptions options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder
            .AddDefaultConfigs()
            .AddNewTypeConfig();
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfig)));

        //modelBuilder.AddGlobalRemoveQueryFilter(this,typeof(IBaseRemovedEntity));
        base.OnModelCreating(modelBuilder);
    }


    #region Unit Of Work

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        this.AddRange(entities);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        this.RemoveRange(entities);
    }

    public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
    {
        MarkAsChanged<TEntity>(entity);
    }

    public DatabaseFacade GetDatabase { get; }

    public async Task<int> ExecuteCommand(string command, params object[] paramters)
    {
        return await this.ExecuteCommand(command, paramters);
    }

    public int SaveAllChanges()
    {
        return this.SaveChanges();
    }

    public Task<int> SaveAllChangesAsync(CancellationToken ct)
    {
        return this.SaveChangesAsync(ct);
    }

    protected virtual  Task BeforeSaving(CancellationToken ct)
    {
        return null;
    }
    
    protected virtual  Task AfterSaving(CancellationToken ct)
    {
        return null;
    }

    #endregion

    
}