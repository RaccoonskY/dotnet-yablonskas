using Dotnet.Web.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Dotnet.Web.Refit;

public interface IUsersController
{
    
    [Get("/Users")]
    Task<UserDto> GetCurrentUser();

    [Post("/Users/Login")]
    Task<LoginResponseDto> Login([FromBody] LoginDto dto);

    [Post("/Users/Register")]
    Task<bool> Register(RegisterDto dto);
    
    [Get("/Users/GetByEmail")]
    Task<UserDto> GetByEmail(string email);
}