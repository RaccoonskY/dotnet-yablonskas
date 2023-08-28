using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;

namespace Dotnet.Web.Tests.Exceptions;

public class ProductControllerTests : WebApplicationFactoryTestsBase<IProductsController>
{
    public ProductControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Validation)]
    public async Task Get_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.Get(9999));
        
        Assert.True(is422);
    }
    
    [Homework(Homeworks.Validation)]
    public async Task GetComments_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.GetComments(9999));
        
        Assert.True(is422);
    }
}