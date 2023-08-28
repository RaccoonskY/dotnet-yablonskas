using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Dotnet.Web.Data;

public static class DbSeeder
{
    private static readonly object Locker = new();

    private static string _adminEmail = "admin@admin.com";
    
    private static string _adminPassword = "123Admin!";
    
    private static string _userEmail = "user@test.com";
    
    private static string _userPassword = "123User!";

    
    public static void Seed(AppDbContext appDbContext, UserManager<User> userManager,
        RoleManager<UserRole> roleManager)
    {
        lock (Locker)
        {
            if (appDbContext.Products.Any())
            {
                return;
            }

            appDbContext.Products.AddRange(Products);
            appDbContext.Comments.AddRange(Comment);
            appDbContext.Users.AddRange(User);
            InitializeUserAsync(appDbContext, userManager, roleManager).Wait();
            InitializeNoUserUserAsync(appDbContext, userManager).Wait();
            appDbContext.SaveChanges();
        }
    }

    private static async Task InitializeUserAsync(AppDbContext context, UserManager<User> userManager, 
        RoleManager<UserRole> roleManager)
    {
        string[] roles = new string[] { "Admin", "User" };

        foreach (string role in roles)
        {
            var ifExistRole = await roleManager.RoleExistsAsync(role);
            if (!ifExistRole)
            {
                await roleManager.CreateAsync(new UserRole()
                {
                    Name = role
                });
            }
        }

        await context.SaveChangesAsync();
        
        var us = await userManager.FindByEmailAsync(_adminEmail);
        if (us == null)
        {
            var user = new User
            {
                Email = _adminEmail,
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "ADMIN",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            await userManager.CreateAsync(user, _adminPassword);
            await context.SaveChangesAsync();
            var newUser = await userManager.FindByEmailAsync(_adminEmail);
            await userManager.AddToRoleAsync(newUser!, "Admin");
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task InitializeNoUserUserAsync(AppDbContext context, UserManager<User> userManager)
    {
        var us = await userManager.FindByEmailAsync(_userEmail);
        if (us == null)
        {
            var user = new User
            {
                Email = _userEmail,
                NormalizedEmail = "USER@TEST.COM",
                UserName = "TESTUSER",
                NormalizedUserName = "TESTUSER",
                PhoneNumber = "+111111111119",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            await userManager.CreateAsync(user, _userPassword);
            await context.SaveChangesAsync();
            var newUser = await userManager.FindByEmailAsync(_userEmail);
            await userManager.AddToRoleAsync(newUser!, "User");
            await context.SaveChangesAsync();
        }
    }

    public static User User { get; } = new()
    {
        Id = 1,
        UserName = "Test",
        Email = "test@test.test",
    };

    public static LoginDto Login { get; } = new() { Email = _adminEmail, Password = _adminPassword };
    
    public static LoginDto LoginByUser { get; } = new() { Email = _userEmail, Password = _userPassword };

    public static List<Product> Products { get; } = new()
    {
        new Product
        {
            Id = 1,
            DiscountPercent = 10,
            Name = "Bread",
            Price = 100
        },
        new Product
        {
            Id = 2,
            DiscountPercent = 10,
            Name = "Water",
            Price = 10
        },
        new Product
        {
            Id = 3,
            DiscountPercent = 5,
            Name = "Cola",
            Price = 20
        }
    };

    public static Cart Cart { get; } = new()
    {
        Id = 1,
        UserId = 1,
        User = User,
    };

    public static Order Order { get; } = new()
    {
        Id = 1,
        UserId = 1,
        OrderStatus = OrderStatus.New,
        Price = 118
    };

    public static Comment Comment = new()
    {
        Id = 1,
        ProductId = 1,
        Rating = 5,
        UserId = 2,
        Text = "Good",
        User = User,
        Product = Products.First()
    };
    
    public static List<OrderProduct> OrderProducts { get; } = new()
    {
        new OrderProduct
        {
            Id = 1,
            ProductId = 1,
            Count = 1,
            Product = Products[0],
            Order = Order,
        },
        new OrderProduct
        {
            Id = 1,
            ProductId = 2,
            Count = 3,
            Product = Products[1],
            Order = Order,
        },
        new OrderProduct
        {
            Id = 1,
            ProductId = 3,
            Count = 5,
            Product = Products[2],
            Order = Order,
        },
    };
}