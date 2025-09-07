namespace SaipaShop.Domain.Exceptions;

public abstract class DomainException(string message,string description) : Exception(message)
{
    public string Description { get; set; } = description;
}