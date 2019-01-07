using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Products
{
    public class ProductViewModel
    {
        public string Id { get; set; }

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
        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        [Range(0, 9999999, ErrorMessage = "Моля въведете валидно число.")]
        public decimal Price { get; set; }

        public bool IsHidden { get; set; }
    }
}