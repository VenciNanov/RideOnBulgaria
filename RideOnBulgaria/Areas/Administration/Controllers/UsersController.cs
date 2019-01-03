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

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRoadsService roadsService;
        private readonly IRoadsIndexService roadsIndexService;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;
        public UsersController(IUsersService usersService, IRoadsService roadsService, IRoadsIndexService roadsIndexService, IOrdersService ordersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.roadsService = roadsService;
            this.roadsIndexService = roadsIndexService;
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return this.Unauthorized();
            }

            var model = new List<UsersTableViewModel>();
            var users = this.usersService.GetAllUsers();

            foreach (var user in users)
            {
                model.Add(new UsersTableViewModel
                {
                    Id = user.Id,
                    Badge = "user",
                    FullName = $"{user.FirstName} {user.LastName}",
                    Orders = /*user.Orders.Count,*/2,
                    PhoneNumber = user.PhoneNumber,
                    Roads = user.Roads.Count,
                    Role = this.usersService.GetUserRole(user.UserName),
                    Username = user.UserName
                });
            }


            return View(model);
        }

        public IActionResult UsersRoads(string id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return this.Unauthorized();
            }

            var model = new UsersRoadsViewModelWrapper();
            var userRoadsViewModels = new List<UsersRoadsViewModel>();
            var roads = this.roadsIndexService.GetCurrentUserRoadsById(id);

            foreach (var road in roads)
            {
                userRoadsViewModels.Add(new UsersRoadsViewModel
                {
                    Id = road.Id,
                    StartingPoint = road.StartingPoint,
                    EndPoint = road.EndPoint,
                    PostedOn = road.PostedOn.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture),
                    Rating = road.AveragePosterRating.ToString("F2"),
                    RoadLength = road.RoadLength,
                    RoadName = road.RoadName
                });
            }

            model.User = this.usersService.GetUserByUsername(this.User.Identity.Name);
            model.UsersRoadsViewModels = userRoadsViewModels;
            return this.View(model);
        }

        public IActionResult UsersOrders(string id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return this.Unauthorized();
            }

            var user = this.usersService.GetUserById(id);
            var orders = this.ordersService.GetCurrentUserOrders(user.UserName);


            var model = new UsersOrdersWrapperViewModel();
            var usersOrdersViewModel = new List<UsersOrdersViewModel>();

            foreach (var order in orders)
            {
                usersOrdersViewModel.Add(mapper.Map<UsersOrdersViewModel>(order));
            }

            model.User = user;
            model.UsersOrders = usersOrdersViewModel;


            return this.View(model);
        }

    }
}