using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class RoadsService : IRoadsService
    {
        private readonly ApplicationDbContext context;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IVideoService videoService;

        public RoadsService(ApplicationDbContext context, IImageService imageService, IMapper mapper, IVideoService videoService)
        {
            this.context = context;
            this.imageService = imageService;
            this.mapper = mapper;
            this.videoService = videoService;
        }

        public bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video, string userId, IFormFile imageFromForm,ICollection<IFormFile> photos)
        {
            if (roadName == null ||
                startingPoint == null ||
                endPoint == null ||
                roadLength == null ||
                description == null ||
                userId == null) return false;

            if (this.context.Roads.Any(x=>x.RoadName==roadName))
            {
                return false;
            }

            string embedYoutubeUrl = null;

            if (video != null)
            {
                embedYoutubeUrl = videoService.ReturnEmbedYoutubeLink(video);
            }

            var imageList = new List<Image>();

            foreach (var photo in photos)
            {
                imageList.Add(this.imageService.AddPhoto(photo));

            }

            Road road = new Road
            {
                RoadName = roadName,
                Description = description,
                EndPoint = endPoint,
                PostedOn = DateTime.UtcNow,
                StartingPoint = startingPoint,
                RoadLength = roadLength,
                Video = embedYoutubeUrl,
                UserId = userId,
                Photos = imageList
            };

            context.Roads.Add(road);
            context.SaveChanges();

            var image = imageService.AddPhoto(imageFromForm);

            image.Name = roadName + "main";

            var coverPhoto = new CoverPhotoRoad
            {
                Image = image,
                ImageId = image.Id,
                Road = road,
                RoadId = road.Id
            };

            

            context.CoverPhotoRoads.Add(coverPhoto);
            context.SaveChanges();

            road.CoverPhoto = coverPhoto;
            road.CoverPhotoId = coverPhoto.Id;

            context.SaveChanges();




            return true;
        }

        public T Details<T>(string id)
        {
            var road = this.context.Roads.Find(id);

            if (road == null)
            {
                return default(T);
            }

            var model = this.mapper.Map<T>(road);

            return model;
        }

        public ICollection<Road> GetRoads()
        {
            return context.Roads.Include(x => x.CoverPhoto).Include(x => x.Photos).Include(x=>x.User).ToList();
        }


    }
}