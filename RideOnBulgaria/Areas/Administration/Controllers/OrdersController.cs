using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Processed()
        {
            return View();
        }

        public IActionResult Sent()
        {
            return View();
        }

        public IActionResult Delivered()
        {
            return View();
        }
    }
}