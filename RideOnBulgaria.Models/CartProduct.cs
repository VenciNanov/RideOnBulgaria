using System;
using System.Collections.Generic;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class CartProduct
    {
       
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
