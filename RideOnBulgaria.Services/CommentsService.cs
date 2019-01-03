using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext context;
        private readonly IRoadsService roadsService;
        private readonly IMapper mapper;

        public CommentsService(IRoadsService roadsService, IMapper mapper, ApplicationDbContext context)
        {
            this.roadsService = roadsService;
            this.mapper = mapper;
            this.context = context;
        }

        public bool AddCommentToRoad(string roadId, User commentator, int rating, string content)
        {
            var road = this.roadsService.GetRoadById(roadId);

            if (road == null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                Rating = rating,
                Road = road,
                RoadId = road.Id,
                Replies = new List<Reply>(),
                User = commentator

            };

            road.Comments.Add(comment);

            this.context.Comments.Add(comment);
            this.context.Update(road);
            this.context.SaveChanges();

            return true;
        }

        public List<Comment> GetCommentsByRoadId(string id)
        {
            var road = this.roadsService.GetRoadById(id);

            if (road == null)
            {
                return null;
            }

            var comments = road.Comments.ToList();



            return comments;
        }

        public bool AddReplyToComment(string commentId, string content, User user)
        {
            var comment = GetCommentById(commentId);

            if (comment==null)
            {
                return false;
            }

            var reply = new Reply
            {
                Comment = comment,
                Content = content,
                User = user
            };

            comment.Replies.Add(reply);
            this.context.Replies.Add(reply);
            this.context.Update(comment);
            this.context.SaveChanges();

            return true;

        }

        public Comment GetCommentById(string commentId)
        {
            var comment = this.context.Comments.FirstOrDefault(x => x.Id == commentId);
            
            return comment;
        }
    }
}