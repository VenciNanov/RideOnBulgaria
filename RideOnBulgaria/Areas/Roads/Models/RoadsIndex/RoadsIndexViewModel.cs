using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex
{
    public class RoadsIndexViewModel
    {
        public ICollection<RoadViewModel> LatestRoads { get; set; }

        public ICollection<RoadViewModel> TopRoads { get; set; }

        public ICollection<RoadViewModel> AllRoads { get; set; }

        public ICollection<RoadViewModel> LongestRoads { get; set; }

    }
}
