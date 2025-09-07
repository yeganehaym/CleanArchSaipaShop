using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Domain.Errors;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(ErrorType.Validation, string.Empty);
    public static readonly Error NullValue = new(ErrorType.Validation, "The specified result value is null.");

    public Error(ErrorType errorType, string message)
    {
        ErrorType = errorType;
        Message = message;
    }
    public Error(ErrorType errorType, string message,string[] parameters)
    {
        ErrorType = errorType;
        Message = message;
        Parameters = parameters;
    }
    public ErrorType ErrorType { get; }

    public string Message { get; }
    public string[] Parameters { get; set; }


    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return ErrorType == other.ErrorType && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(ErrorType, Message);

    public override string ToString() => ErrorType.ToString();
}