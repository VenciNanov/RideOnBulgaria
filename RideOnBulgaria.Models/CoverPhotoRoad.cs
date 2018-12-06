using System;
using System.Collections.Generic;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class CoverPhotoRoad
    {
        public string Id { get; set; }

        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        public string RoadId { get; set; }
        public virtual Road Road { get; set; }
    }
}
