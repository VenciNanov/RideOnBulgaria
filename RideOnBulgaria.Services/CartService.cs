using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext context;
        private readonly IProductsSerivce productsService;
        private readonly IUsersService usersService;

        public CartService(ApplicationDbContext context, IProductsSerivce productsService, IUsersService usersService)
        {
            this.context = context;
            this.productsService = productsService;
            this.usersService = usersService;
        }

        public bool AnyProducts(string username)
        {
            return this.context.CartProducts.Any(x => x.Cart.User.UserName == username);
        }

        public void AddProductToCart(string productId, string username, int? quantity = null)
        {
            var product = this.productsService.GetProductById(productId);

            var user = this.usersService.GetUserByUsername(username);

            if (product == null || user == null)
            {
                return;
            }

            var cartProduct = GetShoppingCartProduct(productId, user.CartId);

            if (cartProduct != null)
            {
                return;
            }

            cartProduct = new CartProduct
            {
                Product = product,
                Quantity = quantity == null ? 1 : quantity.Value,
                CartId = user.CartId
            };

            this.context.CartProducts.Add(cartProduct);
            this.context.SaveChanges();


        }

        public IEnumerable<CartProduct> GetAllCartProducts(string username)
        {
            var user = this.usersService.GetUserByUsername(username);

            if (user == null) return null;

            return this.context.CartProducts.Where(x => x.Cart.User.UserName == username).ToList();
        }

        private CartProduct GetShoppingCartProduct(string productId, string shoppingCartId)
        {
            return this.context.CartProducts.FirstOrDefault(x => x.CartId == shoppingCartId && x.ProductId == productId);
        }

        public void DeleteProductFromCart(string id, string username)
        {
            var product = this.productsService.GetProductById(id);
            var user = this.usersService.GetUserByUsername(username);

            if (product == null || user == null)
            {
                return;
            }

            var shoppingCart = GetShoppingCartProduct(product.Id, user.CartId);

            this.context.CartProducts.Remove(shoppingCart);
            this.context.SaveChanges();
        }

        public void EditProductInCart(string productId, string username, int quantity)
        {
            var product = this.productsService.GetProductById(productId);
            var user = this.usersService.GetUserByUsername(username);

            if (product == null || user == null || quantity <= 0)
            {
                return;
            }

            var shoppingCartProduct = this.GetShoppingCartProduct(productId, user.CartId);
            if (shoppingCartProduct == null)
            {
                return;
            }

            shoppingCartProduct.Quantity = quantity;

            this.context.Update(shoppingCartProduct);
            this.context.SaveChanges();
        }

        public void DeleteAllProductsFromCart(string username)
        {
            var user = this.usersService.GetUserByUsername(username);

            if(user==null)return;


            var cartProducts = this.context.CartProducts.Where(x => x.CartId == user.CartId);

            this.context.CartProducts.RemoveRange(cartProducts);
            this.context.SaveChanges();
        }
    }
}