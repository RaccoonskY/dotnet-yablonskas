using System.ComponentModel.DataAnnotations;

namespace Dotnet.Web.Models;

public class CartProduct
{
    [Key]
    public int Id { get; set; }

    public virtual required Product Product { get; set; }
    
    public int Count { get; set; }
    
    public virtual required Cart Cart { get; set; }
}