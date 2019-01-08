using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models.Products;
using RideOnBulgaria.Web.Common;

namespace RideOnBulgaria.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class ProductsController : Controller
    {
        private const string ProductsDetailsAction = "Details";
        private const string ProductsControllerName = "Products";
        private const string ShopAreaName = "Shop";
        private readonly IProductsSerivce productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsSerivce productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        public IActionResult CreateProduct()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            var product = this.productsService.CreateProduct(model.Name, model.Description, model.Price, model.Image,
                model.AdditionalInfo);

            return this.RedirectToAction(ProductsDetailsAction, ProductsControllerName,
                new {area = ShopAreaName, @id = product.Id});
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
            {
                //TODO
                return NotFound();
            }

            var product = this.productsService.EditProduct(model.Id, model.Name, model.Description, model.IsHidden,
                model.AdditionalInfo, model.Price);

            if (product == null)
            {
                //TODO
                return ValidationProblem();
            }

            return this.RedirectToAction(ProductsDetailsAction, ProductsControllerName,
                new {area = ShopAreaName, @id = model.Id});
        }
    }
}