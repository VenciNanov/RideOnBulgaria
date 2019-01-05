using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Administration.Models.Products;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ProductsController : Controller
    {
        private readonly IProductsSerivce productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsSerivce productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
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

        public IActionResult All()
        {
            var products = this.productsService.GetAllProducts();
            var model = this.mapper.Map<List<ProductViewModel>>(products);

            return this.View(model);
        }

        public IActionResult EditProduct(string id)
        {
            var product = this.productsService.GetProductById(id);
            var model = this.productsService.Details<ProductViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {//TODO
                return NotFound();
            }

            var product = this.productsService.EditProduct(model.Id, model.Name, model.Description, model.IsHidden,
                model.AdditionalInfo, model.Price);

            if (product == null)
            {
                //TODO
                return ValidationProblem();
            }

            return this.RedirectToAction("Details", "Products", new{area="Shop", @id = model.Id });
        }



    }
}