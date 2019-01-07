using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area("Roads")]
    public class CategoriesController : Controller
    {
        private readonly IImageService imageService;
        private readonly IRoadsService roadsService;

        public CategoriesController(IImageService imageService, IRoadsService roadsService)
        {
            this.imageService = imageService;
            this.roadsService = roadsService;
        }

        public IActionResult All()
        {
            var roads = this.roadsService.GetRoads();
            List<RoadViewModel> roadsModel = GetRoadsViewModel(roads);
            return View(roadsModel);
        }

        public IActionResult LatestRoads()
        {
            var roads = this.roadsService.GetLatestRoads();
            List<RoadViewModel> roadsModel = GetRoadsViewModel(roads);

            return View(roadsModel);

        }

        public IActionResult LongestRoads()
        {
            var roads = this.roadsService.GetLongestRoads();
            List<RoadViewModel> roadsModel = GetRoadsViewModel(roads);

            return View(roadsModel);

        }



        public IActionResult TopRoads()
        {
            var roads = this.roadsService.GetTopRoads();
            List<RoadViewModel> roadsModel = GetRoadsViewModel(roads);

            return View(roadsModel);
        }

        private List<RoadViewModel> GetRoadsViewModel(ICollection<RideOnBulgaria.Models.Road> roads)
        {
            var roadsModel = new List<RoadViewModel>();

            foreach (var road in roads)
            {
                var coverPhoto = this.imageService.ReturnImage(road.CoverPhoto.Image);
                roadsModel.Add(new RoadViewModel
                {
                    Id = road.Id,
                    Image = coverPhoto,
                    PostedOn = road.PostedOn,
                    RoadName = road.RoadName,
                    PostedBy = road.User.UserName
                });
            }

            return roadsModel;
        }
    }
}