using System.ComponentModel.DataAnnotations;

namespace Dotnet.Web.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    
    public virtual required User User { get; set; }
    
    public int UserId { get; set; }

    public virtual List<CartProduct> Products { get; set; } = new List<CartProduct>();
}