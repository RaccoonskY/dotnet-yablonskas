using Dotnet.Web.Attributes;
using Dotnet.Web.Dto;
using Dotnet.Web.Validation;
using FluentValidation.TestHelper;

namespace Dotnet.Web.Tests.Validation;

public class AddCommentDtoValidatorTests
{
    private readonly AddCommentDtoValidator _addCommentValidator = new();
    
    [Homework(Homeworks.Validation)]
    public void AddCommentDto_Rating_6_ShouldHaveValidationError()
    {
        var com = new AddCommentDto()
        {
            Rating = 6,
            Text = "1"
        };
        
        var res = _addCommentValidator.TestValidate(com);
        res.ShouldHaveValidationErrorFor(x => x.Rating);
    }
    
    [Homework(Homeworks.Validation)]
    public void AddCommentDto_Text_ToLarge_ShouldHaveValidationError()
    {
        var com = new AddCommentDto()
        {
            Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. " +
                   "Aenean massa. Cim sociis natoque penatibus et magnis dis parturient montes, " +
                   "nascetur ridiculus mus. Donec quam felis,.",
        };
        
        var res = _addCommentValidator.TestValidate(com);
        res.ShouldHaveValidationErrorFor(x => x.Text);
    }
}