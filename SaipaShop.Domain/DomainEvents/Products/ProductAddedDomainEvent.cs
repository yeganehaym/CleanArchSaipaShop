using SaipaShop.Domain.Entities;

namespace SaipaShop.Domain.DomainEvents.Products;

public class ProductAddedDomainEvent:IDomainEvent
{
    public Product Product { get; set; }
    
}