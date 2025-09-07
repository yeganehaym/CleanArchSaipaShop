using SaipaShop.Domain.DomainEvents.Users;
using MediatR;

namespace SaipaShop.Application.CQRS.Notifications.Users;

public class UserLoggedInDomainEventHandler:INotificationHandler<UserLoggedInDomainEvent>
{
    public async Task Handle(UserLoggedInDomainEvent notification, CancellationToken cancellationToken)
    {
        var username = notification.User.Username;
        var time = notification.LoggedInTime.ToString("HH:mm");
        var smsText = $"کاربر {username} در ساعت {time} وارد سیستم شد";
        
        //send sms
        //await sendSms(smsText)
        
    }
}