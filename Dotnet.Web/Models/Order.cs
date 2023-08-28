using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Models;

public class Order
{
    [Key, Required, HiddenInput (DisplayValue = false)] 
    public int Id { get; set; }

    public double Price { get; set; }
    
    public virtual User? User { get; set; }
    
    public int UserId { get; set; }

    public virtual List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    
    public OrderStatus OrderStatus { get; set; }

    [HiddenInput(DisplayValue = false)] 
    public double Total => Products!.Sum(x => x.Product.DiscountedPrice * x.Count);
}
