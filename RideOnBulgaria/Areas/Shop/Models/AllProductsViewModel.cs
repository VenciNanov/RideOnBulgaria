using System.Collections;
using System.Collections.Generic;

namespace RideOnBulgaria.Web.Areas.Shop.Models
{
    public class AllProductsViewModel
    {
        public ICollection<AllProductsDetailsViewModel> AllProductsDetails{ get; set; }
    }
}