using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRoadsService roadsService;

        public UsersController(IUsersService usersService, IRoadsService roadsService)
        {
            this.usersService = usersService;
            this.roadsService = roadsService;
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
            var roads = this.roadsService.GetRoads();

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

    }
}