using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models.Roads;
using RideOnBulgaria.Web.Common;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
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

            var model = mapper.Map<List<RoadsViewModel>>(roads);

            return View(model);
        }
    }
}