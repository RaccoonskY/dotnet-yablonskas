using Dotnet.Web.Attributes;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Refit;
using Dotnet.Web.Tests.Shared;
using FluentAssertions;

namespace Dotnet.Web.Tests.Auth;

public class CommentControllerTests : WebApplicationFactoryTestsBase<ICommentController>
{
    public CommentControllerTests(DotnetWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Homework(Homeworks.Auth)]
    public Task Unauthorized_AddComment_ShouldHave401UnauthorizedResponse()
    {
        var client = CreateControllerClient();

        var comment = DbSeeder.Comment;

        var is401 = IsMethodHave401UnauthorizedResponse(() => client.AddComment(new AddCommentDto
        {
            ProductId = comment.ProductId,
            Rating = comment.Rating,
            Text = comment.Text
        }));
        
        Assert.True(is401);
        return Task.CompletedTask;
    }
    
    [Homework(Homeworks.Auth)]
    public async Task Get_ShouldNotBeEmpty()
    {
        await LoginAsAdminAsync();
        var client = CreateControllerClient();

        var commet = DbSeeder.Comment;
        var commentId = await client.AddComment(new AddCommentDto
        {
            ProductId = commet.ProductId,
            Rating = commet.Rating,
            Text = commet.Text
        });
        
        var commentClient = CreateControllerClient<ICommentController>();
        var comment = await commentClient.Get(commentId);
        comment.Should().NotBeNull();
    }
    
    [Homework(Homeworks.Auth)]
    public async Task AddComment_ShouldAddCorrectData()
    {
        await LoginAsAdminAsync();
        var client = CreateControllerClient();

        var comment = DbSeeder.Comment;
        var commentId = await client.AddComment(new AddCommentDto
        {
            ProductId = comment.ProductId,
            Rating = comment.Rating,
            Text = comment.Text
        });
        
        var commentClient = CreateControllerClient<ICommentController>();
        var commentResponse = await commentClient.Get(commentId);
        Assert.Equal(comment.Rating, commentResponse!.Rating);
    }    
}