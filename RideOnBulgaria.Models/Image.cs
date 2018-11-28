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

        public byte[] Photo { get; set; }

        public string Description { get; set; }
    }
}
