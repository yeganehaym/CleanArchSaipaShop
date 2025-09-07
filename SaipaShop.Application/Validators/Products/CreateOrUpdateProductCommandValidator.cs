using FluentValidation;
using SaipaShop.Application.CQRS.Commands;

namespace SaipaShop.Application.Validators.Products;

public class CreateOrUpdateProductCommandValidator:AbstractValidator<CreateOrUpdateProductCommand>
{
    public CreateOrUpdateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}