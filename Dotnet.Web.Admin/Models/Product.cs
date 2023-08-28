using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Admin.Models
{
    public class Product
    {
        [Key, Required, HiddenInput (DisplayValue = false)] 
        public long Id { get; set; }
        
        [Required]
        public required string Name { get; set; }
        
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;
    }
}