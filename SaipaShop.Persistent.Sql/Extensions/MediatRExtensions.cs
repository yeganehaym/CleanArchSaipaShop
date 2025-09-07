using SaipaShop.Domain.DomainEvents;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SaipaShop.Persistent.Sql.Extensions;

public static class MediatRExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ChangeTracker changeTracker,CancellationToken ct)
    {
        var domainEntities = changeTracker
            .Entries<DomainEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(x => x.GetDomainEvents());

        foreach (var domainEntity in domainEntities)
        {
            await mediator.Publish(domainEntity, ct);
        }
    }
}