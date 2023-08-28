namespace Dotnet.Web.Models;

public class OrderProduct
{
    public int Id { get; set; }
    
    public virtual required Product Product { get; set; }
    
    public int ProductId { get; set; }
    
    public int Count { get; set; }
    
    public virtual required Order Order { get; set; }
}