using Dotnet.Web.Attributes;
using Dotnet.Web.Data;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;

namespace Dotnet.Web.Tests.Auth;

public class CartControllerTests : WebApplicationFactoryTestsBase<ICartController>
{
    public CartControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_UpdateCart_ShouldHave401UnauthorizedResponse()
    {
        var productClient = CreateControllerClient();
        
        var id = DbSeeder.Products.First().Id;
        var is401 = IsMethodHave401UnauthorizedResponse(() => productClient.UpdateCart(id));
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_GetCurrentByUser_ShouldHave401UnauthorizedResponse()
    {
        var productClient = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => productClient.GetCurrentCart());
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_CleanCart_ShouldHave401UnauthorizedResponse()
    {
        var productClient = CreateControllerClient();
        var is401 = IsMethodHave401UnauthorizedResponse(() => productClient.CleanCart());
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public async Task AddToCart_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        
        var id = DbSeeder.Products.First().Id;
        await productClient.UpdateCart(id);

        var cartClient = CreateControllerClient<ICartController>();
        var cart = await cartClient.GetCurrentCart();
        cart.Should().NotBeNull();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task GetCurrentCart_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var cart = await productClient.GetCurrentCart();
        
        cart.Should().NotBeNull();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task CleanCart_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        
        var id = DbSeeder.Products.First().Id;
        await productClient.UpdateCart(id);
        await productClient.CleanCart();
        var cart = await productClient.GetCurrentCart().NotNull();
        
        cart.Products.Should().BeEmpty();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task AddToCart_ShouldAddCorrectData()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        
        var id = DbSeeder.Products.First().Id;
        await productClient.UpdateCart(id);

        var cartClient = CreateControllerClient<ICartController>();
        var cart = await cartClient.GetCurrentCart();
        Assert.Equal(cart!.Products.First().ProductId, id);
    }    
}