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

        [Display(Name = " ")]
        public int Rating { get; set; }

        public User Commentator { get; set; }

        [Required(ErrorMessage = "Моля попълнете полето.")]
        [StringLength(500,ErrorMessage = "Коментарът трябва да е между 10 и 500 символа.",MinimumLength = 10)]
        [Display(Name = "Коментар")]
        public string Comment { get; set; }

    }
}
