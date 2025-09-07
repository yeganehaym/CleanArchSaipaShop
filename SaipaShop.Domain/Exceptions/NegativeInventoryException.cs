namespace SaipaShop.Domain.Exceptions;

public class NegativeInventoryException(string message, string description) : DomainException(message, description)
{
    
}