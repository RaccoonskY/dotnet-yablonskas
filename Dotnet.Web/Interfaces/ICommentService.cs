using Dotnet.Web.Dto;

namespace Dotnet.Web.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetComments(int productId);
    
    Task<CommentDto> GetComment(int id);
    
    Task<int> AddComment(AddCommentDto comment);
}