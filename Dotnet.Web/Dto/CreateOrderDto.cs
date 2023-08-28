using System.ComponentModel.DataAnnotations;

namespace Dotnet.Web.Dto;

public class CreateOrderDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int CartId { get; set; }
}