using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface ICommentsService
    {
        bool AddCommentToRoad(string roadId, User commentator, int rating, string content);
    }
}