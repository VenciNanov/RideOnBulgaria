using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models
{
    public class UsersTableViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Roads { get; set; }

        public int Orders { get; set; }

        public string Role { get; set; }

        public string Badge { get; set; }

        
    }
}
