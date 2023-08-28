using Dotnet.Web.Attributes;
using Dotnet.Web.Dto;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;

namespace Dotnet.Web.Tests.Exceptions;

public class CommentControllerTests : WebApplicationFactoryTestsBase<ICommentController>
{
    public CommentControllerTests(DotnetWebApplicationFactory factory) : base(factory)
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
    public async Task AddComment_IncorrectProductId_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.AddComment(new AddCommentDto
        {
            ProductId = 9999,
            Rating = 5,
            Text = "123123"
        }));
        
        Assert.True(is422);
    }
    
    [Homework(Homeworks.Validation)]
    public async Task AddComment_IncorrectText_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.AddComment(new AddCommentDto
        {
            ProductId = 1,
            Rating = 5,
            Text = "sample text sample text sample text sample text sample text sample text sample text sample text" +
                   "sample text sample text sample text sample text sample text sample text sample text sample text" +
                   "sample text sample text sample text sample text sample text sample text sample text sample text" +
                   "sample text sample text sample text sample text sample text sample text sample text sample text" +
                   "sample text sample text sample text sample text sample text sample text sample text sample text" +
                   "sample text sample text sample text sample text sample text sample text sample text sample text"
        }));
        
        Assert.True(is422);
    }
    
    [Homework(Homeworks.Validation)]
    public async Task AddComment_IncorrectRating_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.AddComment(new AddCommentDto
        {
            ProductId = 1,
            Rating = 7,
            Text = "123123"
        }));
        
        Assert.True(is422);
    }
    
    [Homework(Homeworks.Validation)]
    public async Task AddComment_IncorrectMinusRating_ShouldHave422UnauthorizedResponse()
    {
        await LoginAsAdminAsync();
        var productClient = CreateControllerClient();
        var is422 = IsMethodHaveUserFriendlyException422dResponse(() => productClient.AddComment(new AddCommentDto
        {
            ProductId = 1,
            Rating = -1,
            Text = "123123"
        }));
        
        Assert.True(is422);
    }
}