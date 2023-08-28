using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dotnet.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OrderService(
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

        public async Task<OrderDto> GetOrder()
        {
            var userId = GetUserIdFromClaims();
            var order = await context.Orders
                .Include(order => order.Products)
                    .ThenInclude(prs => prs.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(ord => ord.UserId == userId);
            if (order == null) { throw new BadHttpRequestException("No order", 404); }
            var orderDto = new OrderDto()
            {
                UserId = userId,
                OrderId = order.Id,
                Price = order.Price,
                UserName = order.User.UserName,
                OrderStatus = order.OrderStatus,
                Products = order.Products.ToList().ConvertAll(product =>
                    new ProductListDto()
                    {
                        ProductName = product.Product.Name,
                        ProductId = product.Product.Id,
                        Price = product.Product.Price,
                        Count = product.Count
                    })
            };
            return orderDto;
        }


        public async Task<int> CreateOrder()
        {
            var userId = GetUserIdFromClaims();
            var cart = context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                    .ThenInclude(cpr => cpr.Product)
                .FirstOrDefault(c => c.UserId == userId);
            if (cart == null) { throw new BadHttpRequestException("No cart", 404); }

            var newOrder = new Order()
            {
                Id = context.Orders.Select(o => o.Id).DefaultIfEmpty().Max() + 1,
                UserId = userId,
                Price = cart.Products.Sum(pr => pr.Product.Price * pr.Count),
                User = context.Users.FirstOrDefault(u => u.Id == userId),
                OrderStatus = OrderStatus.New
            };

            var newOrderProductId = context.OrderProducts.Select(o => o.Id).DefaultIfEmpty().Max();
            newOrder.Products = cart.Products.ToList().ConvertAll(product =>
                    new OrderProduct()
                    {
                        Id = ++newOrderProductId,
                        Product = product.Product,
                        ProductId = product.Id,
                        Count = product.Count,
                        Order = newOrder
                    }
            );
            context.Orders.Add(newOrder);
            context.OrderProducts.AddRange(newOrder.Products);
            await context.SaveChangesAsync();
            return newOrder.Id;
        }


        public async Task MoveOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null) { throw new BadHttpRequestException("No such order"); }
            order.OrderStatus = orderStatus;
            await context.SaveChangesAsync();
        }
    }
}
