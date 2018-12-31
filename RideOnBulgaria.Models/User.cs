using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RideOnBulgaria.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Roads = new HashSet<Road>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Road> Roads { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public virtual  ICollection<Order> Orders { get; set; }
    }

}
