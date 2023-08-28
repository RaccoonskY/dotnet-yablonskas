using Dotnet.Web.Dto;
using Refit;

namespace Dotnet.Web.Refit;

public interface ICommentController
{
    [Get("/Comment/{id}")]
    public Task<CommentDto?> Get(int id);

    [Post("/Comment")]
    public Task<int> AddComment(AddCommentDto comment);
}