using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dotnet.Web.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CartService(
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

        public async Task<GetUserCartResponseDto> GetUserCart()
        {
            var userId = GetUserIdFromClaims();
            if (userId == -1) { throw new UnauthorizedAccessException(); }

            var cart = await context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    User = context.Users.FirstOrDefault(u => u.Id == userId)!,
                    Id = context.Carts.Select(c => c.Id).DefaultIfEmpty().Max() + 1,
                    UserId = userId
                };
                context.Carts.Add(cart);

            }
            List<ProductListDto> products = cart.Products.ToList().ConvertAll(product =>
            new ProductListDto()
            {
                ProductName = product.Product.Name,
                ProductId = product.Product.Id,
                Price = product.Product.Price,
                Count = product.Count
            });


            return new GetUserCartResponseDto()
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Products = products,
            };

        }

        public async Task CleanCart()
        {
            var userId = GetUserIdFromClaims();
            if (userId == -1) { throw new UnauthorizedAccessException(); }
            var cart = await context.Carts
                .Include(c => c.User)
                .Include(c => c.Products).
                FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    User = context.Users.FirstOrDefault(u => u.Id == userId)!,
                    Id = context.Carts.Select(c => c.Id).DefaultIfEmpty().Max() + 1,
                    UserId = userId
                };
                context.Carts.Add(cart);
            }
            else
            {
                cart.Products.Clear();
                foreach (var cartProduct in context.CartProducts.Where(cp => cp.Id == cart.Id))
                {
                    context.CartProducts.Remove(cartProduct);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
