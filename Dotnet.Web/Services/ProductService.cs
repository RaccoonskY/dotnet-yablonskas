using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dotnet.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ProductService(
            AppDbContext context,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public int GetUserIdFromClaims()
        {
            var uridClaim = httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
            return (uridClaim != null ? int.Parse(uridClaim.Value) : -1);

        }

        public async Task<Product> GetProduct(long productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(pr => pr.Id == productId);
            if (product == null) { throw new BadHttpRequestException("Product not found", 422); }
            return product;
        }

        public async Task EditProduct(Product product)
        {
            var productToEdit = await context.Products.FirstOrDefaultAsync(pr => pr.Id == product.Id);
            if (productToEdit == null) { throw new BadHttpRequestException("No such product", 404); }
            productToEdit.DiscountPercent = product.DiscountPercent;
            productToEdit.Price = product.Price;
            productToEdit.Name = product.Name;
            await context.SaveChangesAsync();

        }
        public async Task AddProductToCart(int productId)
        {
            var userId = GetUserIdFromClaims();
            var product = context.Products.FirstOrDefault(pr => pr.Id == productId);
            if (product == null )
            {
                throw new BadHttpRequestException("No such product", 422);
            }
            var currentCart = context.Carts.Where(cart => cart.UserId == userId).FirstOrDefault();
            int generateCartProductId = 0;

            if (context.CartProducts.Count() != 0)
            {
                generateCartProductId = context.CartProducts.Select(cartProduct => cartProduct.Id).Max() + 1;
            }

            if (currentCart == null)
            {
                currentCart = CreateCart(userId);

                CartProduct cartProduct = new CartProduct()
                {
                    Id = generateCartProductId,
                    Count = 1,
                    Product = product,
                    Cart = currentCart
                };

                context.CartProducts.Add(cartProduct);
                await context.SaveChangesAsync();

            }
            else
            {
                CartProduct cartProduct = context.CartProducts.Where(cartProduct => cartProduct.Product.Id == product.Id).FirstOrDefault();

                if (cartProduct == null)
                {

                    CartProduct addCartProduct = new CartProduct()
                    {
                        Id = generateCartProductId,
                        Count = 1,
                        Product = product,
                        Cart = currentCart
                    };

                    context.CartProducts.Add(addCartProduct);
                    await context.SaveChangesAsync();
                }
                else
                {
                    cartProduct.Count += 1;
                    await context.SaveChangesAsync();
                }
            }

        }

        private Cart CreateCart(int userId)
        {
            int generateCartId = 0;
            User user = context.Users.Where(user => user.Id == userId).First();
            if (context.Carts.Count() != 0)
            {
                generateCartId = context.Carts.Select(cart => cart.Id).Max() + 1;
            }

            Cart cart = new Cart()
            {
                Id = generateCartId,
                User = user,
                UserId = userId,
                Products = new List<CartProduct>()
            };

            context.Carts.Add(cart);
            context.SaveChanges();

            return cart;
        }

        public async Task AddProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

        }

        public double GetRating(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) { throw new BadHttpRequestException("No such product", 404); }
            var rating = context.Comments.Where(c=>c.ProductId == id).Select(c=>c.Rating).Sum();
            return rating;
        }

        public async Task<IEnumerable<Product>> GetListAsync(PagingDto paging)
        {
            var products = context.Products.OrderBy(p=>p.Id).
                Skip((paging.Page-1)*paging.Take).
                Take(paging.Take);
            return products;
        }
    }
}
