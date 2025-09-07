using System.Transactions;
using MediatR;

namespace SaipaShop.Application.CQRS.Behaviours;

public class TransactionBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };

        using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            TResponse response = await next();

            transaction.Complete();

            return response;
        }
    }
}