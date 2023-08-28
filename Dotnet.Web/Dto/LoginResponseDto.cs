namespace Dotnet.Web.Dto;

public class LoginResponseDto
{
    public required string UserName { get; set; }
    
    public required string Token { get; set; }
}