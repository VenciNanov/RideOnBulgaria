using System;
using System.Collections;
using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class DetailsRoadViewModel
    {
        public string RoadName { get; set; }

        public string StartingPoint { get; set; }

        public string EndPoint { get; set; }

        public double RoadLength { get; set; }

        public string Description { get; set; }

        public Image CoverPhoto { get; set; }

        public string Video { get; set; }

        public ICollection<Image> Images { get; set; }

        public User PostedBy { get; set; }

        public DateTime PostedOn { get; set; }

        public int SurfaceRating { get; set; }

        public int ViewRating { get; set; }

        public int PleasureRating { get; set; }

    }
}