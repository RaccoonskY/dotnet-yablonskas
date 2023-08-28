using Dotnet.Web.Admin.Dto;
using Dotnet.Web.Admin.Models;

namespace Dotnet.Web.Admin.Interfaces;

public interface IUserService
{
    public Task<UserDto> GetUser(int id);
    public Task<User> Login(LoginDto dto);

    public Task<bool> Register(RegisterDto registerDto);
    
    public Task<bool> Register(RegisterDto registerDto, Role role);
    
    public Task<List<UserDto>?> GetUserList();

    public Task DeleteUser(int id);
}