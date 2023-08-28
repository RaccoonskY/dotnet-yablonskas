using Dotnet.Web.Dto;

namespace Dotnet.Web.Interfaces;

public interface ICartService
{
    public Task<GetUserCartResponseDto> GetUserCart();

    public Task CleanCart();
}