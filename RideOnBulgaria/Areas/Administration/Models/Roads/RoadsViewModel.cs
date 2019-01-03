using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Models.Roads
{
    public class RoadsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public User PostedBy { get; set; }

        public int Comments { get; set; }

        public double PosterRating { get; set; }

        public double Rating { get; set; }
    }
}