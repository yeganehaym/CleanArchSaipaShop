namespace SaipaShop.Domain.Exceptions;

public class InvalidPersonnelCode(string message):DomainException(message,"Invalid");