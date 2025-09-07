using SaipaShop.Domain.DomainEvents.Products;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Primitives;

namespace SaipaShop.Domain.Entities;

public class Product:BaseEntity
{
    public string Title { get; set; }
    public Currency Amount { get; set; }
    public int Qty { get; set; }
    public string Description { get; set; }


    public void ProductCreated()
    {
        Raise(new ProductAddedDomainEvent()
        {
            Product = this
        });
    }
}

