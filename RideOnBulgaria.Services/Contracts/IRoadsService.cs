﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IRoadsService
    {
        bool Create(string roadName, string startingPoint, string endPoint, double roadLength, string description, string video,string userId,IFormFile photo);

        T Details<T>(string id);

        ICollection<Road> GetRoads();
    }
}