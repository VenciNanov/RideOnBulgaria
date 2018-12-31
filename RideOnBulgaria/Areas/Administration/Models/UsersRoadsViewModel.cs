using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideOnBulgaria.Web.Areas.Administration.Models
{
    public class UsersRoadsViewModel
    {
        public string Id { get; set; }

        public string RoadName { get; set; }

        public string Rating { get; set; }

        public string PostedOn { get; set; }

        public string StartingPoint { get; set; }

        public string EndPoint { get; set; }

        public double RoadLength { get; set; }
    }
}
