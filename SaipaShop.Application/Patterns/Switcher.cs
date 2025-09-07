using SaipaShop.Domain.Errors;
using SaipaShop.Domain.Exceptions;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.Patterns;

public class Switcher<T>
{
    private readonly List<Func<Result<T>>> _items = new();

    public Switcher<T> Bind(Func<Result<T>> item)
    {
        _items.Add(item);
        return this;
    }

    public Result<T> Start()
    {
        if (_items.Count == 0)
            throw new NullDomainException("items are empty", "There is no items in the async list.");

        foreach (var item in _items)
        {
            var result = item();
            if (result.IsSuccess)
                return result;
        }

        return Result<T>.Failure(DomainErrors.Errors.NoResultFound);
    }
}