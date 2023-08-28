namespace Dotnet.Web.Dto;

public class CommentDto
{
    public long CommentId { get; init; }
    
    public long UserId { get; init; }
    
    public required string? UserName { get; init; } 
        
    public long ProductId { get; init; }
    
    public required string ProductName { get; init; } 
    
    public required string? Text { get; init; }
    
    public int Rating { get; set; }
}