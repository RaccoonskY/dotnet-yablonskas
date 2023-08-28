namespace Dotnet.Web.Admin.Dto;

public class LoginResponseDto
{
    public required string UserName { get; set; }
    
    public required string IsLogin { get; set; }
}