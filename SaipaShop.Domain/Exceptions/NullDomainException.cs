namespace SaipaShop.Domain.Exceptions;

public class NullDomainException(string message, string description) : DomainException(message, description)
{
    
}