using Dotnet.Web.Dto;
using FluentValidation;

namespace Dotnet.Web.Validation;

public class RegisterDtoValidator : AbstractValidator<RegisterDto> 
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Password)
            .Must((x, pass) => !x.Email.Contains(pass) && !pass.Contains(x.Email));

        RuleFor(x => x.Email)
            .Must((x, email) => !x.Password.Contains(email) && !email.Contains(x.Password));

    }
}