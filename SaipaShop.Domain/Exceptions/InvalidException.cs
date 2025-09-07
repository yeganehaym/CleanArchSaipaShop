namespace SaipaShop.Domain.Exceptions;

public class InvalidException(string message):DomainException(message,"IsInvalid");