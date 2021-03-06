﻿using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IRoadsService
    {
        bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video,string userId,IFormFile photo,ICollection<IFormFile> photos, int viewRating, int surfaceRating, int pleasureRating);

        T Details<T>(string id);

        Road GetRoadById(string id);

        ICollection<Road> GetRoads();

        ICollection<Road> GetLatestRoads();

        ICollection<Road> GetLongestRoads();

        ICollection<Road> GetTopRoads();

        Road GetRoadByImage(Image image);

        bool Edit(string roadId, string roadName, string startingPoint, string endPoint, double roadLength,
            string description, string video, IFormFile imageFromForm,
            int viewRating, int surfaceRating, int pleasureRating);

        bool AddImagesToRoad(ICollection<IFormFile> images, string roadId);

        bool DeleteRoad(string id);
    }
}