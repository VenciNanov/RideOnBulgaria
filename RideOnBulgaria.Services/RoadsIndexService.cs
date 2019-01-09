using RideOnBulgaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using RideOnBulgaria.Data;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class RoadsIndexService : IRoadsIndexService
    {
        private const int ImagesCountForCarousel = 5;

        private readonly ApplicationDbContext context;

        public RoadsIndexService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ICollection<Road> GetAllRoads()
        {
            var roads = this.context.Roads.Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Road> GetLatestRoads()
        {
            var roads = this.context.Roads.OrderByDescending(x => x.PostedOn).Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Road> GetLongestRoads()
        {
            var roads = this.context.Roads.OrderByDescending(x => x.RoadLength).Take(ImagesCountForCarousel).ToList();

            return roads;
        }

        public ICollection<Road> GetTopRoads()
        {
            var roads = this.context.Roads.OrderByDescending(x => x.AverageRating)
                .Take(ImagesCountForCarousel)
                .ToList();

            return roads;
        }

        public ICollection<Road> GetCurrentUserRoadsById(string id)
        {
            var roads = this.context.Roads.Where(x => x.User.Id == id).ToList();
            return roads;
        }
    }
}
