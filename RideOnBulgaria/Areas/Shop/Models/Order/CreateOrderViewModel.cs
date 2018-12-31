using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RideOnBulgaria.Web.Areas.Shop.Models.Order
{
    public class CreateOrderViewModel
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        public string AdditionalInformation { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
