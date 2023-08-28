using Dotnet.Web.Models;

namespace Dotnet.Web.Interfaces;

public interface  IJwtGeneratorService
{
    Task<string> CreateToken(User user);
}