using SaipaShop.Domain.Errors;

namespace SaipaShop.Domain.ResultPattern;

public class Result
{
    public Error Error { get; protected set; }
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
}

public sealed class Result<T>:Result
{
    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public T Value { get; }
   

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);
    
    public static Result<T> Create<T>(T? value) => value is not null ? Result<T>.Success(value) : Result<T>.Failure(Error.NullValue);
}
