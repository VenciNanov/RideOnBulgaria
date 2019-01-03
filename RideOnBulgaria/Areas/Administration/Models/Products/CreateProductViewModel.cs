using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Products
{
    public class CreateProductViewModel
    {
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

        [Required]
        [Display(Name = "Picture of the product")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Range(0, 9999999, ErrorMessage = "Please enter valid number.")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Products count")]
        [Range(0,int.MaxValue,ErrorMessage = "Please enter a valid number.")]
        public int Count { get; set; }

    }
}