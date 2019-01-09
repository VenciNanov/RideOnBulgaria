using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Administration.Models.Users;
using RideOnBulgaria.Web.Common;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRoadsService roadsService;
        private readonly IRoadsIndexService roadsIndexService;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, IRoadsService roadsService,
            IRoadsIndexService roadsIndexService, IOrdersService ordersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.roadsService = roadsService;
            this.roadsIndexService = roadsIndexService;
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var users = this.usersService.GetAllUsers();

            var model = mapper.Map<List<UsersTableViewModel>>(users);

            foreach (var user in model)
            {
                user.Role = this.usersService.GetUserRole(user.Username);
            }

            return View(model);
        }

        public IActionResult UsersRoads(string id)
        {
            var roads = this.roadsIndexService.GetCurrentUserRoadsById(id);

            var model = new UsersRoadsViewModelWrapper
            {
                User = this.usersService.GetUserById(id),
                UsersRoadsViewModels = mapper.Map<List<UsersRoadsViewModel>>(roads)
            };

            return this.View(model);
        }

        public IActionResult UsersOrders(string id)
        {
            var user = this.usersService.GetUserById(id);
            var orders = this.ordersService.GetCurrentUserOrders(user.UserName);

            var model = new UsersOrdersWrapperViewModel
            {
                User = this.usersService.GetUserById(id),
                UsersOrders = mapper.Map<List<UsersOrdersViewModel>>(orders)
            };

            return this.View(model);
        }

        public IActionResult PromoteUser(string id)
        {

            bool isPromoted = this.usersService.PromoteUserToAdminRole(id);

            if (!isPromoted)
            {
                return NotFound();
            }

            return this.RedirectToAction("All", "Users");
        }

        public IActionResult DemoteUser(string id)
        {
            bool isDemoted = this.usersService.DemoteUserToUserRole(id);

            if (!isDemoted)
            {
                return NotFound();
            }

            return this.RedirectToAction("All", "Users");
        }
    }
}