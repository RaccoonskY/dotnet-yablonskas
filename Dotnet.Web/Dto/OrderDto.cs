using Dotnet.Web.Models;

namespace Dotnet.Web.Dto;

public class OrderDto
{
    public long OrderId { get; set; }

    public double Price { get; set; }
    
    public long UserId { get; set; }
    
    public required string UserName { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    
    public required List<ProductListDto> Products { get; set; }
}