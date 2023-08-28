using Dotnet.Web.Admin.Interfaces;
using Dotnet.Web.Admin.Models;

namespace Dotnet.Web.Admin.Services;

public class ProductService : IProductService
{
    public Task<List<Product>?> GetProductList()
    {
        throw new NotImplementedException();
    }

    public Task AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}