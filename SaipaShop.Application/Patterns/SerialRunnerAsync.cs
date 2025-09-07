using SaipaShop.Domain.Exceptions;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.Patterns;

public class SerialRunnerAsync<T>
{
    private readonly Queue<Func<Task<Result<T>>>> _asyncItems = new();

    public SerialRunnerAsync<T> EnqueueAsync(Func<Task<Result<T>>> item)
    {
        _asyncItems.Enqueue(item);
        return this;
    }

    public async Task<Result<T>> StartAsync()
    {
        if (_asyncItems.Count == 0)
            throw new NullDomainException("items are empty", "There is no items in the list.");

        Result<T> response = null;
        foreach (var asyncItem in _asyncItems)
        {
            response = await asyncItem();
            if (!response.IsSuccess)
                return response;
        }

        return response;
    }
}