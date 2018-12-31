using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models
{
    public class UsersRoadsViewModelWrapper
    {
        public User User { get; set; }

        public ICollection<UsersRoadsViewModel> UsersRoadsViewModels { get; set; }
    }
}