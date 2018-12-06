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
            var roadsModel = new List<RoadViewModel>();
            
            foreach (var road in roads)
            {
                var mainImage = this.imageService.ReturnImage(road.CoverPhoto.Image);

                roadsModel.Add(new RoadViewModel
                {
                    Id = road.Id,
                    Image = mainImage,
                    PostedOn = road.PostedOn,
                    RoadName = road.RoadName,
                    PostedBy = this.User.Identity.Name
                });
            }
            return View(roadsModel);
        }

        public IActionResult LatestRoads()
        {
            return View();

        }

        public IActionResult TopRoads()
        {
            return View();
        }
    }
}