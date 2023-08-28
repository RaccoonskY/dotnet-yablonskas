namespace Dotnet.Web.Dto;

public class ProductListDto
{
    public int ProductId { get; set; }
    
    public required string ProductName { get; set; }
    
    public double Price { get; set; } 
    
    public int Count { get; set; }
}