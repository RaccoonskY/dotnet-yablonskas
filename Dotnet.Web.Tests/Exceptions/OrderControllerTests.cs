using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;

namespace Dotnet.Web.Tests.Exceptions;

public class OrderControllerTests : WebApplicationFactoryTestsBase<IOrderController>
{
    public OrderControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Validation)]
    public async Task UpdateOrderStatus_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.UpdatePay(9999));
        
        Assert.True(is422);
    }
}