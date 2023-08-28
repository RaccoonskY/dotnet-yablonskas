namespace Dotnet.Web.Dto;

public class GetUserCartResponseDto
{
    public long CartId { get; set; }
    
    public int UserId { get; set; }
    
    public required List<ProductListDto> Products { get; set; }
}


