using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Products
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description about the product")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Additional Info about the product")]
        public string AdditionalInfo { get; set; }

        
        public ProductImage Image { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Range(0, 9999999, ErrorMessage = "Please enter valid number.")]
        public decimal Price { get; set; }

        public bool IsHidden { get; set; }
    }
}