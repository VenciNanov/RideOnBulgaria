using System;
using System.Collections.Generic;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class Trip
    {
        public Trip()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos=new HashSet<Image>();
        }

        public string Id { get; set; }

        public string TripName { get; set; }

        public string StartingPoint { get; set; }

        public string EndPoint { get; set; }

        public double TripLength { get; set; }

        public int Days { get; set; }

        public ICollection<Image> Photos { get; set; }

        public string Description { get; set; }
    }
}
