using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models;
using RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex;
using RideOnBulgaria.Web.Common;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area(Constants.RoadsArea)]
    public class CategoriesController : Controller
    {
        private readonly IImageService imageService;
        private readonly IRoadsService roadsService;
        private readonly IMapper mapper;

        public CategoriesController(IImageService imageService, IRoadsService roadsService, IMapper mapper)
        {
            this.imageService = imageService;
            this.roadsService = roadsService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var roads = this.roadsService.GetRoads();
            List<RoadViewModel> roadsModel = MapRoadsViewModel(roads);
            return View(roadsModel);
        }

        public IActionResult LatestRoads()
        {
            var roads = this.roadsService.GetLatestRoads();
            List<RoadViewModel> roadsModel = MapRoadsViewModel(roads);

            return View(roadsModel);
        }

        public IActionResult LongestRoads()
        {
            var roads = this.roadsService.GetLongestRoads();
            List<RoadViewModel> roadsModel = MapRoadsViewModel(roads);

            return View(roadsModel);
        }

        public IActionResult TopRoads()
        {
            var roads = this.roadsService.GetTopRoads();
            List<RoadViewModel> roadsModel = MapRoadsViewModel(roads);

            return View(roadsModel);
        }

        private List<RoadViewModel> MapRoadsViewModel(ICollection<RideOnBulgaria.Models.Road> roads)
        {
            var roadsModel = new List<RoadViewModel>();

            foreach (var road in roads)
            {
                var coverPhoto = this.imageService.ReturnImage(road.CoverPhoto.Image);
                var token = mapper.Map<RoadViewModel>(road);
                token.CoverPhoto = coverPhoto;
                roadsModel.Add(token);
            }

            return roadsModel;
        }
    }
}