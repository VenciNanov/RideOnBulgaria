using System;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class RoadViewModel
    {
        public string Id { get; set; }

        public string RoadName { get; set; }

        public string Image { get; set; }

        public DateTime PostedOn { get; set; }

        public string PostedBy { get; set; }
    }
}