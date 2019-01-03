using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments
{
    public class CommentPostViewModel
    {
        public string RoadId { get; set; }

        public int Rating { get; set; }

        public User Commentator { get; set; }

        public string Comment { get; set; }

    }
}
