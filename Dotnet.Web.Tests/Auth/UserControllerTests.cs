using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;

namespace Dotnet.Web.Tests.Auth;

public class UserControllerTests : WebApplicationFactoryTestsBase<IUsersController>
{
    public UserControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }

    [Homework(Homeworks.Auth)]
    public Task Unauthorized_GetCurrentUser_ShouldHave401UnauthorizedResponse()
    {
        var client = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => client.GetCurrentUser());
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_GetByEmail_ShouldHave401UnauthorizedResponse()
    {
        var client = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => client.GetByEmail(""));
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public async Task AuthorizedByUser_GetByEmail_ShouldHave423UnauthorizedResponse()
    {
        var client = CreateControllerClient();
        await LoginAsUserAsync();
        var is401 = IsMethodHave403UnauthorizedResponse(() => client.GetByEmail("test"));
        
        Assert.True(is401);
    }
}