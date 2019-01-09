using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Roads.Models.RoadsIndex;

namespace RideOnBulgaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoadsIndexService roadsService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public HomeController(IRoadsIndexService roadsService, IImageService imageService, IMapper mapper)
        {
            this.roadsService = roadsService;
            this.imageService = imageService;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            var roads = this.roadsService.GetTopRoads();

            var model = new List<RoadViewModel>();

            foreach (var road in roads)
            {
                var coverPhoto = this.imageService.ReturnImageWithGiverDimensions(road.CoverPhoto.Image,1500,300,"limit");
                var token = mapper.Map<RoadViewModel>(road);
                token.CoverPhoto = coverPhoto;
                model.Add(token);
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult TrafficInformation()
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:53662/api/PathInfo").Result;
            var products = response.Content.ReadAsAsync<List<TrafficInformation>>().Result;
            return View(products);
        }
    }
}
