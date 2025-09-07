using SaipaShop.Domain.Repositories;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class UsernameExistenceValidator<T,TProperty>:AsyncPropertyValidator<T,TProperty>
{
    private IUserRepository _userRepository;

    public UsernameExistenceValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    

    public override async Task<bool> IsValidAsync(ValidationContext<T> context, TProperty value, CancellationToken cancellation)
    {
        if (value == null)
            return true;
        
        var username = value.ToString();
        if (string.IsNullOrEmpty(username))
            return true;


        var usernameIsExists = await _userRepository.UserExistsAsync(username);

        return usernameIsExists;
    }

    public override string Name => "UsernameExistenceValidator";
}

public static class UsernameExistenceValidatorExtension
{
    public static IRuleBuilderOptions<T, TProperty?> SetUsernameExistenceValidator<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder,IUserRepository userRepository)
    {
        return ruleBuilder.SetAsyncValidator(new UsernameExistenceValidator<T, TProperty>(userRepository)!);
    }
}