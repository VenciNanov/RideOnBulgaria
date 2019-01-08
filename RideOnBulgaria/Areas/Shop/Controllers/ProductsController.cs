using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Shop.Models;
using RideOnBulgaria.Web.Common;

namespace RideOnBulgaria.Web.Areas.Shop.Controllers
{
    [Area(Constants.ShopArea)]
    public class ProductsController : Controller
    {
        private readonly IProductsSerivce productsService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public ProductsController(IProductsSerivce productsService, IImageService imageService, IMapper mapper)
        {
            this.productsService = productsService;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var products = this.productsService.GetAllProducts();

            var model = new AllProductsViewModel();
            var allProductDetailsViewModel = model.AllProductsDetails = new List<AllProductsDetailsViewModel>();

            foreach (var product in products)
            {
                var image = this.imageService.ReturnProductImage(product.Image);
                var token = mapper.Map<AllProductsDetailsViewModel>(product);
                token.Image = image;
                allProductDetailsViewModel.Add(token);
            }

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var product = this.productsService.GetProductById(id);

            if (product == null) return Redirect("Error");

            var model = this.productsService.Details<ProductDetailsViewModel>(product.Id);

            return this.View(model);
        }
    }
}