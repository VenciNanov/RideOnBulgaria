using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IImageService imageService;

        public CategoriesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public IActionResult All()
        {
            return View();
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