using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments
{
    public class CommentPostViewModel
    {
        public string RoadId { get; set; }

        [Required(ErrorMessage = "Please rate the road")]
        public int Rating { get; set; }

        public User Commentator { get; set; }

        [Required(ErrorMessage = "Please fill in the field.")]
        [StringLength(500,ErrorMessage = "Comment should be between 10 or 500 words.",MinimumLength = 10)]
        public string Comment { get; set; }

    }
}
