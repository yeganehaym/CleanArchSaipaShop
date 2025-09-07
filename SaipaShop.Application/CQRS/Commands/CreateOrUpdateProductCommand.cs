using MapsterMapper;
using MediatR;
using SaipaShop.Application.Common;
using SaipaShop.Application.Dto.Products;
using SaipaShop.Domain.DomainEvents.Products;
using SaipaShop.Domain.Entities;
using SaipaShop.Domain.Errors;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.CQRS.Commands;

public class CreateOrUpdateProductCommand:ProductDto,ICommand<ProductDto>
{
    
}

public class CreateOrUpdateProductCommandHandler:ICommandHandler<CreateOrUpdateProductCommand,ProductDto>
{
    private IUnitOfWork _uow;
    private IGenericRepository<Product> _repository;
    private IMapper _mapper;
    private IMediator _mediator;

    public CreateOrUpdateProductCommandHandler(IUnitOfWork uow, IGenericRepository<Product> repository
        , IMapper mapper, IMediator mediator)
    {
        _uow = uow;
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public  async Task<Result<ProductDto>> Handle(CreateOrUpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = null;

        if (request.Id > 0)
        {
            //Edit
            product =await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                return Result<ProductDto>.Failure(DomainErrors.Errors.NoResultFound);
            }
        }
        else
        {
            //create
            product = new();
            await _repository.CreateAsync(product);
        }

        _mapper.Map(request, product);

        var rows = await _uow.SaveAllChangesAsync(cancellationToken);

        if (rows > 0)
        {
           // product.ProductCreated();
           await _mediator.Publish(new ProductAddedDomainEvent()
           {
               Product = product
           });
            var dto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(dto);
        }

        return Result<ProductDto>.Failure(DomainErrors.Errors.AddedFailed);
    }
}