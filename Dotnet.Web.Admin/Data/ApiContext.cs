using Dotnet.Web.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Web.Admin.Data;

public class ApiContext : IdentityDbContext<User, UserRole, int>
{
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
    }
    
    public ApiContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public virtual DbSet<Product> Products => Set<Product>();

}
