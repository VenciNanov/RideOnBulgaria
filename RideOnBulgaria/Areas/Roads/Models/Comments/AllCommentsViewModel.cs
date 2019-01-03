using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments
{
    public class AllCommentsViewModel
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}