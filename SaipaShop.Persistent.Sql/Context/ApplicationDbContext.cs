using SaipaShop.Domain.Entities.Users;
using SaipaShop.Persistent.Sql.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Persistent.Sql.Context;

/// <summary>
/// put dbsets here
/// </summary>
public class ApplicationDbContext:MyDbContextBase
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
            
    }
    
    
    //================ Users ===============================
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    
    
    //========== Events ==================

    protected override async Task AfterSaving(CancellationToken ct)
    {
        await base.AfterSaving(ct);
        var mediateR = this.GetInfrastructure().GetRequiredService<IMediator>();

        await MediatRExtensions.DispatchDomainEventsAsync(mediateR, ChangeTracker, ct);
    }
}