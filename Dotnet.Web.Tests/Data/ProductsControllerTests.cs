using Dotnet.Web.Attributes;
using Dotnet.Web.Controllers;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;
using Refit;

namespace Dotnet.Web.Tests.Data;

public class ProductsControllerTests: WebApplicationFactoryTestsBase<IProductsController>
{
    public ProductsControllerTests(DotnetWebApplicationFactory factory) : base(factory) { }
    
    [Homework(Homeworks.Data)]
    public async Task Get_ShouldNotBeEmpty()
    {
        var client = CreateControllerClient();

        var products = await client.Get(new PagingDto());
        products.Should().NotBeEmpty();
    }

    [Homework(Homeworks.Data)]
    public async Task GetById_ShouldNotBeEmpty()
    {
        var client = RestService.For<IProductsController>(HttpClient);

        var products = await Task.WhenAll(DbSeeder
            .Products
            .Select(x => client.Get(x.Id)));

        products.Should().AllSatisfy(x => x.Should().NotBeNull());
    }

    [Homework(Homeworks.Data)]
    public async Task GetComments_ShouldNotBeEmpty()
    {
        var productClient = CreateControllerClient();
        var comments = await productClient.GetComments(DbSeeder.Comment.ProductId);
        comments.Should().NotBeEmpty();
    }
    
    [Homework(Homeworks.Data)]
    public async Task PagingGet_ShouldNotBeEmpty()
    {
        var productClient = CreateControllerClient();
        var comments = await productClient.Get(new PagingDto());
        comments.Should().NotBeEmpty();
    }
    
    [Homework(Homeworks.Data)]
    public async Task GetById_ShouldGetCorrectData()
    {
        var client = RestService.For<IProductsController>(HttpClient);
        var id = DbSeeder.Products.First().Id;
        var product = await client.Get(id);

        Assert.Equal(id, product.Id);
    }

    [Homework(Homeworks.Data)]
    public async Task GetComments_ShouldGetCorrectData()
    {
        var productClient = CreateControllerClient();
        var comments = await productClient.GetComments(DbSeeder.Comment.ProductId);
        Assert.Equal(DbSeeder.Comment.Text , comments.First().Text);
    }
}