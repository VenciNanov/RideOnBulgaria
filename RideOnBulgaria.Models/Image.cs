using System;
using System.Collections.Generic;
using System.Text;

namespace RideOnBulgaria.Models
{
    public class Image
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name{ get; set; }

        public string PublicId { get; set; }

        public string ImageUrl { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public string RoadId { get; set; }
        public virtual Road Road { get; set; }

        //Posted by
        public string UserId { get; set; }
        public virtual User User { get; set; }


    }
}
