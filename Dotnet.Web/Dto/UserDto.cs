namespace Dotnet.Web.Dto;

public class UserDto
{
    public long UserId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}