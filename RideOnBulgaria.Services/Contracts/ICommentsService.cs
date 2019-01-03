using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface ICommentsService
    {
        bool AddCommentToRoad(string roadId, User commentator, int rating, string content);

        List<Comment> GetCommentsByRoadId(string id);
        Comment GetCommentById(string commentId);

        bool AddReplyToComment(string commentId, string content, User user);
    }
}