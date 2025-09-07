using SaipaShop.Domain.ResultPattern;
using MediatR;

namespace SaipaShop.Application.CQRS;

public interface ICommand<T>:IRequest<Result<T>>
{
    
}