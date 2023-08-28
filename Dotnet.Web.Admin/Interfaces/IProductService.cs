using Dotnet.Web.Admin.Models;

namespace Dotnet.Web.Admin.Interfaces;

public interface IProductService
{

    public Task<List<Product>?> GetProductList();
    public Task AddProduct(Product product);

    public Task DeleteProduct(int id);
}