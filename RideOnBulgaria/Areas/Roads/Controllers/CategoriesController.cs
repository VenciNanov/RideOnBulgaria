using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    public class CategoriesController : Controller
    {
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