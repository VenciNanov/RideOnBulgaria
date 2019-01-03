using System.Collections.Generic;

namespace RideOnBulgaria.Models
{
    public class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public string Id { get; set; }

        public string RoadId { get; set; }

        public virtual Road Road { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public bool IsDeleted { get; set; }
    }
}