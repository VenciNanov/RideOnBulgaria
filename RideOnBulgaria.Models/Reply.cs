namespace RideOnBulgaria.Models
{
    public class Reply
    {
        public string Id { get; set; }

        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}