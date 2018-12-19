using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ProductsController : Controller
    {
        private readonly IProductsSerivce productsService;

        public ProductsController(IProductsSerivce productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            var product =
                this.productsService.CreateProduct(model.Name, model.Description, model.Price, model.Count, model.Image,
                    model.AdditionalInfo);

            return this.Redirect("Shop/Products/Details/" + product.Id);
        }



    }
}