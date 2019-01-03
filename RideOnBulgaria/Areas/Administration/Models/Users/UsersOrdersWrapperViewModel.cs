using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Users
{
    public class UsersOrdersWrapperViewModel
    {
        public User User { get; set; }

        public ICollection<UsersOrdersViewModel> UsersOrders { get; set; }
    }
}