using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Users
{
    public class UsersRoadsViewModelWrapper
    {
        public User User { get; set; }

        public ICollection<UsersRoadsViewModel> UsersRoadsViewModels { get; set; }
    }
}