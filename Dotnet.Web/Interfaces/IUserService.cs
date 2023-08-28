
using Dotnet.Web.Dto;

namespace Dotnet.Web.Interfaces;

public interface IUserService
{
    public Task<UserDto> GetUser();

    public Task<LoginResponseDto> Login(LoginDto dto);

    public Task<bool> Register(RegisterDto registerDto);
    
    public Task<UserDto> GetUserByEmail(string email);
}