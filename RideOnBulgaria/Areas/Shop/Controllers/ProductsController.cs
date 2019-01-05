using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Administration.Models;
using RideOnBulgaria.Web.Areas.Shop.Models;

namespace RideOnBulgaria.Web.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class ProductsController : Controller
    {
        private readonly IProductsSerivce productsService;
        private readonly IImageService imageService;

        public ProductsController(IProductsSerivce productsService, IImageService imageService)
        {
            this.productsService = productsService;
            this.imageService = imageService;
        }

        public IActionResult All()
        {
            var products = this.productsService.GetAllProducts();

            var model = new AllProductsViewModel();
            var allProductDetailsViewModel = model.AllProductsDetails = new List<AllProductsDetailsViewModel>();

            foreach (var product in products)
            {
                //allProductDetailsViewModel.Add(this.productsService.IndexProductDetails<AllProductsDetailsViewModel>(product.Id));
                allProductDetailsViewModel.Add(new AllProductsDetailsViewModel
                {
                    Id = product.Id,
                    Image = this.imageService.ReturnProductImage(product.Image),
                    Name = product.Name,
                    Price = product.Price,
                    IsHidden = product.IsHidden
                });
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