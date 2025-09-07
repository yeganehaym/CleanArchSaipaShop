using System.Net.Mail;
using SaipaShop.Domain.Exceptions;

namespace SaipaShop.Domain.Primitives;

public class Email
{
    public string EmailAddress { get; private set; }

    private  Email(string email)
    {
        EmailAddress = email;
    }

    public static Email Create(string email)
    {
        if (String.IsNullOrEmpty(email))
        {
            throw new EmptyException(nameof(EmailAddress));
        }

        if (!IsEmailValid(email))
        {
            throw new InvalidException(nameof(EmailAddress));
        }
        
        return new Email(email);
    }

    public static bool IsEmailValid(string email)
    {
        MailAddress mailAddress;
        return MailAddress.TryCreate(email, out mailAddress);
    }
 
}