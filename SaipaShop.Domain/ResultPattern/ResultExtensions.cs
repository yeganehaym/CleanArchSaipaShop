using SaipaShop.Domain.Errors;

namespace SaipaShop.Domain.ResultPattern;

public static class ResultExtensions
{
 
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        if (!predicate(result.Value))
            return Result<T>.Failure(error);

        return result;
    }

    public static Result<Tout> Map<Tin, Tout>(this Result<Tin> result, Func<Tin, Tout> mappingFunc)
    {
        return result.IsSuccess ? Result<Tout>.Success(mappingFunc(result.Value)) : Result<Tout>.Failure(result.Error);
    }
    
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
    {
        return result.IsSuccess ?
            bind(result.Value) :
            Result<TOut>.Failure(result.Error);
    }

    public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func, Error error)
    {
        try
        {
            return result.IsSuccess ?
                Result<TOut>.Success(func(result.Value)) :
                Result<TOut>.Failure(result.Error);
        }
        catch
        {
            return Result<TOut>.Failure(error);
        }
    }

    public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }

        return result;
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ?
            onSuccess(result.Value) :
            onFailure(result.Error);
    }
    
    public static void MatchAction<T>(
        this Result<T> result,
        Action<T> onSuccess,
        Action<Error> onFailure)
    {
        if (result.IsSuccess)
        {
            if (onSuccess!= null)
            {
                onSuccess(result.Value);
            }
        }
        else
        {
            if (onFailure!= null)
            {
                onFailure(result.Error);
            }
        }
    }
    
    
}