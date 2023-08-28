using Dotnet.Web.Models;
using FluentValidation;

namespace Dotnet.Web.Validation;

public class ProductValidator : AbstractValidator<Product> 
{
    public ProductValidator() {
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.DiscountPercent).InclusiveBetween(0,100);
    
    }
}