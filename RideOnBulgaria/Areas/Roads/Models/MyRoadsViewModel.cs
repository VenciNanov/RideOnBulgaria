using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class MyRoadsViewModel
    {
        public string Id { get; set; }

        public string CoverPhotoUrl { get; set; }

        public DateTime PostedOn { get; set; }

        public string RoadName { get; set; }
    }
}
