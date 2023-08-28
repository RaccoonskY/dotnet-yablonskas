using Dotnet.Web.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Dotnet.Web.Refit;

public interface ICartController
{
    [Get("/Cart")]
    Task<GetUserCartResponseDto?> GetCurrentCart();

    [Delete("/Cart")]
    Task CleanCart();
    
    [Put("/Cart/{productId}")]
    Task UpdateCart([FromRoute]int productId);
}