using Dotnet.Web.Attributes;
using Dotnet.Web.Models;
using Dotnet.Web.Validation;
using FluentValidation.TestHelper;

namespace Dotnet.Web.Tests.Validation;

public class ProductValidatorTests
{
    private readonly ProductValidator _validator = new();
    
    [Homework(Homeworks.Validation)]
    public void Product_DiscountPercent_110_ShouldHaveValidationError()
    {
        var prod = new Product
        { 
            Id = 1,
            Name = "test",
            DiscountPercent = 110
        };
        
        var res = _validator.TestValidate(prod);
        res.ShouldHaveValidationErrorFor(x => x.DiscountPercent);
    }
    
    [Homework(Homeworks.Validation)]
    public void Product_Price_Minus10_ShouldHaveValidationError()
    {
        var prod = new Product
        { 
            Id = 1,
            Name = "test",
            Price = -10
        };
        
        var res = _validator.TestValidate(prod);
        res.ShouldHaveValidationErrorFor(x => x.Price);
    }
}