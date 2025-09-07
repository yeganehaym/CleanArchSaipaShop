using MediatR;
using SaipaShop.Domain.DomainEvents.Products;

namespace SaipaShop.Application.CQRS.Notifications.Products;

public class ProductAddedSmsDomainEventHandler:INotificationHandler<ProductAddedDomainEvent>
{
    public Task Handle(ProductAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var path = @"D:\notif\";
        Directory.CreateDirectory(path);
        var filePath = Path.Combine(path, "sms.txt");
        File.WriteAllText(filePath,$"Title:{notification.Product.Title} Added at {notification.Product.CreationDate.ToString("yyyy/MM/dd HH:mm:ss")}");
        
        return Task.CompletedTask;

    }
}

public class ProductAddedEmailDomainEventHandler:INotificationHandler<ProductAddedDomainEvent>
{
    public Task Handle(ProductAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var path = @"D:\notif\";
        Directory.CreateDirectory(path);
        var filePath = Path.Combine(path, "email.txt");
        File.WriteAllText(filePath,$"Title:{notification.Product.Title} Added at {notification.Product.CreationDate.ToString("yyyy/MM/dd HH:mm:ss")}");

return Task.CompletedTask;
    }
}