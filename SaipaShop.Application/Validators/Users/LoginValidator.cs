using SaipaShop.Application.Dto.Users;
using FluentValidation;

namespace SaipaShop.Application.Validators.Users;

public class LoginValidator:AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

