using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RideOnBulgaria.Services.Contracts;
using RideOnBulgaria.Web.Areas.Shop.Models;
using RideOnBulgaria.Web.Common;
using RideOnBulgaria.Web.Helpers;

namespace RideOnBulgaria.Web.Areas.Shop.Controllers
{
    [Area(Constants.ShopArea)]
    public class CartController : Controller
    {
        private const string CartJson = "cart";
        private readonly ICartService cartService;
        private readonly IProductsSerivce productsService;
        private readonly IMapper mapper;

        public CartController(ICartService cartService, IProductsSerivce productsService, IMapper mapper)
        {
            this.cartService = cartService;
            this.productsService = productsService;
            this.mapper = mapper;
        }

        public IActionResult MyCart()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var cartProducts = this.cartService.GetAllCartProducts(this.User.Identity.Name);


                var viewModel = mapper.Map<List<CartProductsViewModel>>(cartProducts);


                return this.View(viewModel);
            }
           

            var cart = SessionHelper.GetObjectFromJson<List<CartProductsViewModel>>(HttpContext.Session, CartJson);

            if (cart==null)
            {
                cart = new List<CartProductsViewModel>();
            }

            return View(cart);
        }

        [Authorize]
        public IActionResult AddProduct(string id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.cartService.AddProductToCart(id, this.User.Identity.Name);
                return this.RedirectToAction(nameof(MyCart));
            }

            List<CartProductsViewModel> cart = SessionHelper.GetObjectFromJson<List<CartProductsViewModel>>(HttpContext.Session, CartJson);

            if (cart == null)
            {
                cart = new List<CartProductsViewModel>();
            }

            if (!cart.Any(x => x.Id == id))
            {
                var product = this.productsService.GetProductById(id);

                var shoppingCart = mapper.Map<CartProductsViewModel>(product);
                shoppingCart.Quantity = 1;
                shoppingCart.TotalPrice = shoppingCart.Quantity * shoppingCart.Price;

                cart.Add(shoppingCart);

                SessionHelper.SetObjectAsJson(HttpContext.Session, CartJson, cart);
            }

            return this.RedirectToAction(nameof(MyCart));
        }

        public IActionResult Edit(string id, int quantity)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.cartService.EditProductInCart(id, this.User.Identity.Name, quantity);
                return this.RedirectToAction(nameof(MyCart));
            }

            List<CartProductsViewModel> cart = SessionHelper.GetObjectFromJson<List<CartProductsViewModel>>(HttpContext.Session, CartJson);
            if (cart == null)
            {
                cart = new List<CartProductsViewModel>();
            }

            if (cart.Any(x => x.Id == id) && quantity >= 0)
            {
                var product = cart.First(x => x.Id == id);
                product.Quantity = quantity;
                product.TotalPrice = quantity * product.Price;

                SessionHelper.SetObjectAsJson(HttpContext.Session, CartJson, cart);
            }

            return this.RedirectToAction(nameof(MyCart));
        }

        public IActionResult Delete(string id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.cartService.DeleteProductFromCart(id, this.User.Identity.Name);

                return this.RedirectToAction(nameof(MyCart));
            }

            List<CartProductsViewModel> cart = SessionHelper.GetObjectFromJson<List<CartProductsViewModel>>(HttpContext.Session, CartJson);
            if (cart == null)
            {
                cart = new List<CartProductsViewModel>();
            }

            if (cart.Any(x => x.Id == id))
            {
                var product = cart.First(x => x.Id == id);
                cart.Remove(product);

                SessionHelper.SetObjectAsJson(HttpContext.Session, CartJson, cart);
            }

            return this.RedirectToAction(nameof(MyCart));
        }
    }
}