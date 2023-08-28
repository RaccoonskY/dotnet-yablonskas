using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Data;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;

namespace Dotnet.Web.Tests.Auth;

public class OrderControllerTests : WebApplicationFactoryTestsBase<IOrderController>
{
    public OrderControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_Get_ShouldHave401UnauthorizedResponse()
    {
        var client = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => client.Get());
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_MoveStatus_ShouldHave401UnauthorizedResponse()
    {
        var client = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => client.UpdatePay(1));
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public async Task AuthorizedAsUser_MoveStatus_ShouldHave401UnauthorizedResponse()
    {
        await LoginAsUserAsync();
        var client = CreateControllerClient();
        var is401 = IsMethodHave403UnauthorizedResponse(() => client.UpdatePay(1));
        
        Assert.True(is401);
    }
    
    [Homework(Homeworks.Auth)]
    public async Task Get_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var client = CreateControllerClient();
        var cartClient = CreateControllerClient<ICartController>();
        
        var id = DbSeeder.Products.First().Id;
        await cartClient.UpdateCart(id);
        _ = await cartClient.GetCurrentCart();
        await client.Create();
        var order = await client.Get();
        order.Should().NotBeNull();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task MoveStatus_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var client = CreateControllerClient();
        var cartClient = CreateControllerClient<ICartController>();
        
        var id = DbSeeder.Products.First().Id;
        await cartClient.UpdateCart(id);
        _ = await cartClient.GetCurrentCart();
        var orderId = await client.Create();
        await client.UpdatePay(orderId);
        var order = await client.Get();
        order.Should().NotBeNull();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task UpdateCart_ShouldAddCorrectData()
    {
        await LoginAsAdminAsync();
        var client = CreateControllerClient();
        var cartClient = CreateControllerClient<ICartController>();
        
        var id = DbSeeder.Products.First().Id;
        await cartClient.UpdateCart(id);
        _ = await cartClient.GetCurrentCart();
        await client.Create();
        var order = await client.Get();
        Assert.Equal(id, order.Products.First().ProductId);
    }    
}