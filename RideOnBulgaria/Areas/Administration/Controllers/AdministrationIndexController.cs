using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdministrationIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}