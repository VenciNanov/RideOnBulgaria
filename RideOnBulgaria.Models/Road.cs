﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class Road
    {
        public Road()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos = new HashSet<Image>();
        }

        public string Id { get; set; }

        public string RoadName { get; set; }

        public string StartingPoint { get; set; }

        public string EndPoint { get; set; }

        public double RoadLength { get; set; }

        //public string CoverId { get; set; }
        //public virtual Image Cover { get; set; }

        public string CoverPhotoId { get; set; }
        public virtual CoverPhotoRoad CoverPhoto { get; set; }

        public virtual ICollection<Image> Photos { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime PostedOn { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}