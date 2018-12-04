using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class RoadsService : IRoadsService
    {
        private readonly ApplicationDbContext context;
        private readonly IImageService imageService;

        public RoadsService(ApplicationDbContext context, IImageService imageService)
        {
            this.context = context;
            this.imageService = imageService;
        }

        public bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video, string userId,IFormFile imageFromForm)
        {
            if (roadName == null ||
                startingPoint == null ||
                endPoint == null ||
                roadLength == null ||
                description == null ||
                userId == null) return false;


            Road road = new Road
            {
                RoadName = roadName,
                Description = description,
                EndPoint = endPoint,
                PostedOn = DateTime.UtcNow,
                StartingPoint = startingPoint,
                RoadLength = roadLength,
                Video = video,
                UserId = userId
            };

            var image = imageService.AddPhoto(imageFromForm);

            road.Photos.Add(image);
            

            context.Roads.Add(road);
            context.SaveChanges();

            return true;
        }
    }
}