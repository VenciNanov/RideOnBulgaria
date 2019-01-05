using System.ComponentModel.DataAnnotations;

namespace RideOnBulgaria.Web.Areas.Roads.Models.Comments.Replies
{
    public class ReplyPostViewModel
    {
        [Required(ErrorMessage = "Please fill in the field")]
        [StringLength(500,ErrorMessage = "Reply should be between 2 and 500 words.",MinimumLength = 2)]
        public string Content { get; set; }
        
    }
}