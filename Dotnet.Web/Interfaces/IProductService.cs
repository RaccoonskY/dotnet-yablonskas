using Dotnet.Web.Dto;
using Dotnet.Web.Models;

namespace Dotnet.Web.Interfaces;

public interface IProductService
{
    Task<Product> GetProduct(long productId);

    Task EditProduct(Product product);

    Task AddProductToCart(int productId);

    Task AddProduct(Product product);
    
    double GetRating(int id);
    
    Task<IEnumerable<Product>> GetListAsync(PagingDto paging);
}