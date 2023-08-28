using System.ComponentModel.DataAnnotations;

namespace Dotnet.Web.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    public virtual required User User { get; set; }
    
    public int UserId { get; set; }
    
    public virtual required Product Product { get; set; }
    
    public int ProductId { get; set; }
    
    public string? Text { get; set; }
    
    public int Rating { get; set; }
}