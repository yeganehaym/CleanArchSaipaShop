namespace SaipaShop.Domain.Exceptions;

public class EmptyException(string message):DomainException(message,"IsRequired");