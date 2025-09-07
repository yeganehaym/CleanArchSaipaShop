using SaipaShop.Domain.ResultPattern;
using MediatR;

namespace SaipaShop.Application.CQRS;

public interface IQuery<T>:IRequest<Result<T>>
{
    
}