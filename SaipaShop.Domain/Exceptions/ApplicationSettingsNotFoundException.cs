namespace SaipaShop.Domain.Exceptions;

public class ApplicationSettingsNotFoundException(string message, string description) : DomainException(message, description)
{
    
}