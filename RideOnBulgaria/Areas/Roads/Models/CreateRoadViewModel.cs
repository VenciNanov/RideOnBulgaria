using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models
{
    public class CreateRoadViewModel
    {
        public string Id { get; set; }

        [Required]
        public string TripName { get; set; }

        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public double TripLength { get; set; }

        public virtual ICollection<Image> Photos { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Url]
        public string Video { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
