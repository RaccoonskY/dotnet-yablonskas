using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Models
{
    public class Product
    {
        [Key, Required, HiddenInput (DisplayValue = false)] 
        public int Id { get; set; }
        
        [Required]
        public required string Name { get; set; }

        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;
    }
}