using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area("Roads")]
    
    public class RoadsController : Controller
    {
        private readonly IRoadsService roadsService;
        private readonly IImageService imageService;

        public RoadsController(IRoadsService roadsService, IImageService imageService)
        {
            this.roadsService = roadsService;
            this.imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }

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

            this.roadsService.Create(model.TripName, model.StartingPoint, model.EndPoint, model.TripLength, model.Description,
                 model.Video,userId, model.Photo);


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

        public IActionResult Road()
        {
            return this.View();
        }

        public IActionResult UploadImage()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult UploadImage(UploadImageViewModel model)
        {
            //var imgId = imageService.UploadImage(model.Path);

            //return this.RedirectToAction("ShowPicture");
            return this.View();

        }

        public IActionResult ShowPicture()
        {
            return this.View();
        }
    }
}