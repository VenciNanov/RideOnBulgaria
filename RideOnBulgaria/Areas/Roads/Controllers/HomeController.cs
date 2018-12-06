using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models;
using RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex;
using RoadViewModel = RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex.RoadViewModel;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area("Roads")]
    
    public class HomeController : Controller
    {
        private readonly IRoadsService roadsService;
        private readonly IImageService imageService;
        private readonly IRoadsIndexService roadsIndexService;

        public HomeController(IRoadsService roadsService, IImageService imageService, IRoadsIndexService roadsIndexService)
        {
            this.roadsService = roadsService;
            this.imageService = imageService;
            this.roadsIndexService = roadsIndexService;
        }

        public IActionResult Index()
        {
            var model = new RoadsIndexViewModel();

            var allRoads = this.roadsIndexService.GetAllRoads().ToList();
            var allRoadsModel = model.AllRoads = new List<RoadViewModel>();
            
            foreach (var road in allRoads)
            {
                model.AllRoads.Add(new RoadViewModel
                {
                    Id = road.Id,
                    CoverPhoto = imageService.ReturnImage(road.CoverPhoto.Image),
                    PostedOn = road.PostedOn,
                    PostedBy = road.User.UserName,
                    RoadName = road.RoadName
                });
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateRoadViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

          var result =  this.roadsService.Create(model.TripName, model.StartingPoint, model.EndPoint, model.TripLength, model.Description,
                 model.Video,userId, model.CoverPhoto,model.Images);

            if (!result)
            {
                return this.RedirectToAction("Error");
            }


            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult EditRoad()
        {
            return this.View();
        }

        public IActionResult DeleteRoad()
        {
            return this.View();
        }

        public IActionResult Road(string id)
        {
            var model = this.roadsService.Details<DetailsRoadViewModel>(id);

            if (model==null)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        public IActionResult UploadImage()
        {
            return this.View();
        }

        //[HttpPost]
        //public IActionResult UploadImage(UploadImageViewModel model)
        //{
        //    //var imgId = imageService.UploadImage(model.Path);

        //    //return this.RedirectToAction("ShowPicture");
        //    return this.View();

        //}

        public IActionResult ShowPicture()
        {
            return this.View();
        }
    }
}