using SaipaShop.Domain.Errors;
using SaipaShop.Domain.Exceptions;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.Patterns;

public class SwitcherAsync<T>
{
    private readonly List<Func<Task<Result<T>>>> _asyncItems = new();

    public SwitcherAsync<T> BindAsync(Func<Task<Result<T>>> item)
    {
        _asyncItems.Add(item);
        return this;
    }

    public async Task<Result<T>> StartAsync()
    {
        if (_asyncItems.Count == 0)
            throw new NullDomainException("items are empty", "There is no items in the list.");

        foreach (var item in _asyncItems)
        {
            var result = await item();
            if (result.IsSuccess)
                return result;
        }

        return Result<T>.Failure(DomainErrors.Errors.NoResultFound);
    }
}