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
    public class RoadsIndexService:IRoadsIndexService
    {
        private const int ImagesCountForCarousel = 5;

        private readonly ApplicationDbContext context;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public RoadsIndexService(ApplicationDbContext context, IImageService imageService, IMapper mapper)
        {
            this.context = context;
            this.imageService = imageService;
            this.mapper = mapper;
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
            var roads = this.context.Roads.OrderByDescending(x => x.AveragePosterRating).Take(ImagesCountForCarousel).ToList();

            return roads;
        }
    }
}
