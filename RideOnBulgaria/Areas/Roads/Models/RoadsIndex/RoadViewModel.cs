using System;

namespace RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex
{
    public class RoadViewModel
    {
        public string Id { get; set; }

        public string RoadName { get; set; }

        public string CoverPhoto { get; set; }

        public DateTime PostedOn { get; set; }

        public string PostedBy { get; set; }
    }
}