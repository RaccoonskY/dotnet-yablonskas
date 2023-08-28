using Dotnet.Web.Dto;
using Dotnet.Web.Validation;
using Dotnet.Web.Attributes;
using FluentValidation.TestHelper;

namespace Dotnet.Web.Tests.Validation;

public class RegisterDtoValidatorTests
{
    private readonly RegisterDtoValidator _validator = new();
    
    [Homework(Homeworks.Validation)]
    public void RegisterDto_WrongEmail_ShouldHaveValidationError()
    {
        var com = new RegisterDto
        {
            Email = "123123",
            UserName = "test",
            Password = "12312312"
        };

        var res = _validator.TestValidate(com);

        res.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Homework(Homeworks.Validation)]
    public void RegisterDto_WrongPassword_ShouldHaveValidationError()
    {
        var com = new RegisterDto
        {
            Email = "123123",
            UserName = "test",
            Password = "12312312"
        };

        var res = _validator.TestValidate(com);

        res.ShouldHaveValidationErrorFor(x => x.Password);
    }
}