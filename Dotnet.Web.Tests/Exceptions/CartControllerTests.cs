using Dotnet.Web.Attributes;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;

namespace Dotnet.Web.Tests.Exceptions;

public class CartControllerTests : WebApplicationFactoryTestsBase<ICartController>
{
    public CartControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Validation)]
    public async Task UpdateCart_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.UpdateCart(9999));
        
        Assert.True(is422);
    }
}