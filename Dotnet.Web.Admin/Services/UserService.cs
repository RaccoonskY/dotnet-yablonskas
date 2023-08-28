using Dotnet.Web.Admin.Dto;
using Dotnet.Web.Admin.Interfaces;
using Dotnet.Web.Admin.Models;

namespace Dotnet.Web.Admin.Services;

public class UserService : IUserService
{
    public Task<UserDto> GetUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Login(LoginDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterDto registerDto, Role role)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserDto>?> GetUserList()
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}