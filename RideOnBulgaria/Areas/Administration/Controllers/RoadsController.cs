using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models.Roads;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class RoadsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRoadsIndexService roadsIndexService;

        public RoadsController(IRoadsIndexService roadsIndexService, IMapper mapper)
        {
            this.roadsIndexService = roadsIndexService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var roads = this.roadsIndexService.GetAllRoads();



            var model = new List<RoadsViewModel>();

            foreach (var road in roads)
            {
                model.Add(mapper.Map<RoadsViewModel>(road));
            }

            return View(model);
        }
    }
}