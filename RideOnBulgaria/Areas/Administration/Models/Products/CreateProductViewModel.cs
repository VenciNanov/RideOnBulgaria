using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Products
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Text)]
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание на продукта")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Допълнителна информация")]
        public string AdditionalInfo { get; set; }

        [Required]
        [Display(Name = "Снимка на продукта")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        [Range(0, 9999999, ErrorMessage = "Моля въведете валидно число.")]
        public decimal Price { get; set; }

    }
}