using System.Collections.Generic;
using AutoMapper;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class CommentsService:ICommentsService
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

            if (road==null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                Rating = rating,
                Road = road,
                RoadId = road.Id,
                Replies = new List<Reply>()
            };

            road.Comments.Add(comment);

            this.context.Comments.Add(comment);
            this.context.Update(road);
            this.context.SaveChanges();

            return true;
        }
    }
}