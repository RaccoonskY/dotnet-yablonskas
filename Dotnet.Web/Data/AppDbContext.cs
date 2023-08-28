using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Web.Data;

public class AppDbContext : IdentityDbContext<User, UserRole, int>
{
    private readonly IWebHostEnvironment _env;

    public AppDbContext(DbContextOptions options, IWebHostEnvironment env) : base(options)
    {
        _env = env;
    }

    public virtual DbSet<Product> Products => Set<Product>();
    
    public virtual DbSet<CartProduct> CartProducts => Set<CartProduct>();
    
    public virtual DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    
    public virtual DbSet<Order> Orders => Set<Order>();
    
    public virtual DbSet<Cart> Carts => Set<Cart>();
    
    public virtual DbSet<Comment> Comments  => Set<Comment>();
}