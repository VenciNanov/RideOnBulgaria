using System.Collections;
using System.Collections.Generic;

namespace RideOnBulgaria.Models
{
    public class Cart
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; }
    }
}