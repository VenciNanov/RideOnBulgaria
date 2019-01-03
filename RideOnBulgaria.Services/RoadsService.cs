using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        private readonly IVideoService videoService;
        private readonly IUsersService usersService;

        public RoadsService(ApplicationDbContext context, IImageService imageService, IMapper mapper, IVideoService videoService, UserManager<User> userManager, IUsersService usersService)
        {
            this.context = context;
            this.imageService = imageService;
            this.mapper = mapper;
            this.videoService = videoService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public bool Edit(string roadId, string roadName, string startingPoint, string endPoint, double roadLength,
            string description, string video, IFormFile imageFromForm,
            int viewRating, int surfaceRating, int pleasureRating)
        {
            if (roadName == null ||
                startingPoint == null ||
                endPoint == null ||
                roadLength == null ||
                description == null) return false;

            //if (this.context.Roads.Any(x => x.RoadName == roadName))
            //{
            //    return false;
            //}

            //if (this.context.Roads.Any(x => x.RoadName == roadName)) return false;

            string embedYoutubeUrl = null;

            if (video != null)
            {
                embedYoutubeUrl = videoService.ReturnEmbedYoutubeLink(video);
            }

            var road = this.context.Roads.FirstOrDefault(x => x.Id == roadId);
            if (road == null) return false;

            road.RoadName = roadName;
            road.Description = description;
            road.StartingPoint = startingPoint;
            road.EndPoint = endPoint;
            road.RoadLength = roadLength;
            road.SurfaceRating = surfaceRating;
            road.ViewRating = viewRating;
            road.PleasureRating = pleasureRating;

            if (road.Video != video)
            {
                road.Video = embedYoutubeUrl;
            }


           // context.Roads.Add(road);
            context.SaveChanges();

            if (imageFromForm != null)
            {
                road.CoverPhoto = null;

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
            }

            return true;
        }

        public Road GetRoadById(string id)
        {
            var road = this.context.Roads.FirstOrDefault(x => x.Id == id);
            return road;
        }

        public bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video, string userId, IFormFile imageFromForm, ICollection<IFormFile> photos, int viewRating, int surfaceRating, int pleasureRating)
        {
            if (roadName == null ||
                startingPoint == null ||
                endPoint == null ||
                roadLength == null ||
                description == null ||
                userId == null ||
                imageFromForm == null) return false;

            if (this.context.Roads.Any(x => x.RoadName == roadName))
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
                Photos = imageList,
                SurfaceRating = surfaceRating,
                ViewRating = viewRating,
                PleasureRating = pleasureRating
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
            return context.Roads.Include(x => x.CoverPhoto).Include(x => x.Photos).ToList();
        }

        public ICollection<Road> GetLatestRoads()
        {
            return this.context.Roads.Include(x => x.CoverPhoto).Include(x => x.Photos).Include(x => x.User)
                .OrderByDescending(x => x.PostedOn).ToList();
        }
        
        public ICollection<Road> GetLongestRoads()
        {
            return this.context.Roads.Include(x => x.CoverPhoto).Include(x => x.Photos).Include(x => x.User)
                .OrderByDescending(x => x.RoadLength).ToList();
        }

        public Road GetRoadByImage(Image image)
        {
            var img = this.context.Images.First(x => x.Id == image.Id);
            var road = this.context.Roads.Find(img.Road.Id);
            return road;
        }

        public bool AddImagesToRoad(ICollection<IFormFile> images,string roadId)
        {
            var road = this.GetRoadById(roadId);

            if (road == null) return false;

            var imageList = new List<Image>();

            foreach (var image in images)
            {
                //imageList.Add(this.imageService.AddPhoto(image));
                road.Photos.Add(this.imageService.AddPhoto(image));
            }

            this.context.SaveChanges();

            return true;
        }

        public bool DeleteRoad(string id , ClaimsPrincipal userClaims)
        {
            var road = this.context.Roads.FirstOrDefault(x => x.Id == id);
            //var userId = this.userManager.GetUserId(userClaims);
            //var user = this.usersService.GetUserById(userId);


            if (road == null) return false;

            var coverPhotoRoad = this.context.CoverPhotoRoads.FirstOrDefault(x => x.RoadId == road.Id);

            this.context.CoverPhotoRoads.Remove(coverPhotoRoad);
            this.context.Roads.Remove(road);
            this.context.SaveChanges();

            return true;
        }



    }
}