using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RideOnBulgaria.Web.Areas.Roads.Controllers
{
    [Area("Roads")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return this.View();
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
    }
}