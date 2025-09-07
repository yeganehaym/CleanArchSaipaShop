namespace SaipaShop.Infrastructure.MediatR.Behaviors.Caching;

public interface ICachableQuery
{
    public string Key { get;}
}