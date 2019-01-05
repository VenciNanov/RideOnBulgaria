using System;
using System.Collections.Generic;
using RideOnBulgaria.Models;
using RideOnBulgaria.Web.Areas.Roads.Models.Comments.Replies;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments
{
    public class AllCommentsViewModel
    {
        public string Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public ICollection<ReplyViewModel> ReplyViewModel { get; set; }

        public DateTime PostedOn { get; set; }

        public bool IsDeleted { get; set; }

    }
}