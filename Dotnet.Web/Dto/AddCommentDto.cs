using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Dto;

public class AddCommentDto
{
    public AddCommentDto() { }

    public AddCommentDto(int productId, string? text, int rating)
    {
        ProductId = productId;
        Text = text;
        Rating = rating;
    }

    [Required]
    public int ProductId { get; set; }
    
    public string? Text { get; set; }
    
    public int Rating { get; set; }
}