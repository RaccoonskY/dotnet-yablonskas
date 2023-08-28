using Dotnet.Web.Dto;
using FluentValidation;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Dotnet.Web.Validation;

public class AddCommentDtoValidator : AbstractValidator<AddCommentDto> 
{
    public AddCommentDtoValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Text).Length(0,200);
    }
}