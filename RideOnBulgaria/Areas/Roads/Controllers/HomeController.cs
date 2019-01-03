using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Models;
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

            var latestRoads = this.roadsIndexService.GetLatestRoads();
            var latestRoadsModel = model.LatestRoads = new List<RoadViewModel>();

            foreach (var road in latestRoads)
            {
                model.LatestRoads.Add(new RoadViewModel
                {
                    Id = road.Id,
                    CoverPhoto = imageService.ReturnImage(road.CoverPhoto.Image),
                    PostedOn = road.PostedOn,
                    PostedBy = road.User.UserName,
                    RoadName = road.RoadName
                });
            }

            var longestRoads = this.roadsIndexService.GetLongestRoads();
            var longestRoadsModel = model.LongestRoads = new List<RoadViewModel>();

            foreach (var road in longestRoads)
            {
                model.LongestRoads.Add(new RoadViewModel
                {
                    Id = road.Id,
                    CoverPhoto = imageService.ReturnImage(road.CoverPhoto.Image),
                    PostedOn = road.PostedOn,
                    PostedBy = road.User.UserName,
                    RoadName = road.RoadName
                });
            }

            var topRoads = this.roadsIndexService.GetTopRoads();
            var topRoadsModel = model.TopRoads = new List<RoadViewModel>();

            foreach (var road in topRoads)
            {
                model.TopRoads.Add(new RoadViewModel
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

            var result = this.roadsService.Create(model.TripName, model.StartingPoint, model.EndPoint, model.TripLength, model.Description,
                   model.Video, userId, model.CoverPhoto, model.Images, model.View, model.Surface, model.Pleasure);

            if (!result)
            {
                return this.RedirectToAction("Error");
            }


            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult EditRoad(string id)
        {
            var road = this.roadsService.GetRoadById(id);
            
                if (road == null)
                {
                    return NotFound();
                }


            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            

            if (userId != road.UserId||!this.User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            //if (this.User.Identity!=road.User)
            //{
            //    return Unauthorized();
            //}

            var model = new EditRoadViewModel
            {
                TripName = road.RoadName,
                Description = road.Description,
                StartingPoint = road.StartingPoint,
                EndPoint = road.EndPoint,
                Id = id,
                Surface = road.SurfaceRating,
                View = road.ViewRating,
                Pleasure = road.PleasureRating,
                Images = road.Photos,
                TripLength = road.RoadLength,
                Video = road.Video,
            };


            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditRoad(EditRoadViewModel model)
        {
            var road = this.roadsService.GetRoadById(model.Id);

            if (road == null) return NotFound();


            var result = this.roadsService.Edit(model.Id, model.TripName, model.StartingPoint, model.EndPoint,
                model.TripLength, model.Description, model.Video, model.CoverPhoto, model.View, model.Surface,
                model.Pleasure);

            return this.RedirectToAction("MyRoads", "Home");
        }


        public IActionResult EditRoadPictures(string id)
        {
            var road = this.roadsService.GetRoadById(id);

            var model = new EditRoadViewModel
            {
                Id=road.Id,
                Images = road.Photos
            };
            

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddImagesToRoad(EditRoadViewModel model)
        {
            var road = this.roadsService.GetRoadById(model.Id);

            if (model.NewImages == null)
            {
                return RedirectToAction("Error");
            }

            var images = this.roadsService.AddImagesToRoad(model.NewImages, road.Id);

            return this.Redirect($"/Roads/Home/EditRoadPictures/{road.Id}");

        }

        public IActionResult DeleteRoadPictures(string id)
        {
            var image = this.imageService.FindImageById(id);

            var road = this.roadsService.GetRoadByImage(image);

            this.imageService.RemoveImage(image);

            return this.Redirect($"/Roads/Home/EditRoadPictures/{road.Id}");
        }

        public IActionResult DeleteRoad(string id)
        {
            ClaimsPrincipal user = this.User;

            bool result = roadsService.DeleteRoad(id,user);
            
            return this.RedirectToAction("DeltedRoadSuccesfully", "Home");
        }

        public IActionResult DeltedRoadSuccesfully()
        {
            return this.View();
        }

        public IActionResult Road(string id)
        {
            var model = this.roadsService.Details<DetailsRoadViewModel>(id);

            if (model == null)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult MyRoads()
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserRoads = this.roadsIndexService.GetCurrentUserRoadsById(currentUser);

            var model = new List<MyRoadsViewModel>();

            foreach (var road in currentUserRoads)
            {
                model.Add(new MyRoadsViewModel
                {
                    Id = road.Id,
                    RoadName = road.RoadName,
                    CoverPhotoUrl = this.imageService.ReturnImage(road.CoverPhoto.Image),
                    PostedOn = road.PostedOn,
                });
            }

            return this.View(model);
        }

        //[HttpPost]
        //public IActionResult UploadImage(UploadImageViewModel model)
        //{
        //    //var imgId = imageService.UploadImage(model.Path);

        //    //return this.RedirectToAction("ShowPicture");
        //    return this.View();

        //}


    }
}