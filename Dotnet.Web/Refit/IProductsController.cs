using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Dotnet.Web.Refit;

public interface IProductsController
{
    [Get("/Products")]
    Task<IEnumerable<Product>> Get([FromQuery] PagingDto paging);
    
    [Get("/Products/{id}")]
    Task<Product> Get([FromRoute]int id);
    
    [Get("/Products/{productId}/comments")]
    Task<IEnumerable<CommentDto>> GetComments([FromRoute] int productId);
}   