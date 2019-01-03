using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Administration.Models.Users;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {

        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

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
            return this.View();
        }

        public IActionResult Details(string id)
        {
           var orderProducts = this.ordersService.GetOrderDetails(id);
            var model = new List<OrderDetailsViewModel>();

            foreach (var product in orderProducts)
            {
                model.Add(mapper.Map<OrderDetailsViewModel>(product));
            }

            return this.View(model);
        }

        public IActionResult Deliver(string id)
        {
            this.ordersService.DeliverOrder(id);

            return this.RedirectToAction("Index", "AdministrationIndex");
        }

        public IActionResult Send(string id)
        {
            this.ordersService.SendOrder(id);

            return this.RedirectToAction("Index","AdministrationIndex");
        }
    }
}