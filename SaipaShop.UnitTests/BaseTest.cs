using System.Reflection;
using System.Windows.Input;
using SaipaShop.Domain.DomainEvents;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using SaipaShop.Infrastructure.Services;
using SaipaShop.Infrastructure.Services.Globalization;
using SaipaShop.Persistent.Sql.Context;

namespace SaipaShop.UnitTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(DateTimeProvider).Assembly;
    protected static readonly Assembly PersistentAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(IEndpoint).Assembly;
    
    public static T AssertDomainEventWasPublished<T>(DomainEntity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

        if (domainEvent == null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}
