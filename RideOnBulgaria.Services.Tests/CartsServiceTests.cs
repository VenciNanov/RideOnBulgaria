using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class CartsServiceTests
    {
        [Fact]
        public void AnyProductToCartShouldAddProductToCart()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CartsService")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new User
            {
                UserName = "username",
                Cart = new Cart()
            };

            dbContext.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                .Returns(dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName));

            var productId = "productId1";
            var productService = new Mock<IProductsSerivce>();
            productService.Setup(x => x.GetProductById(productId))
                .Returns(new Product {Name = "Ball"});

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.AddProductToCart(productId, user.UserName);

            var cartProducts = dbContext.CartProducts.ToList();

            Assert.Single(cartProducts);
            Assert.Equal(user.CartId, cartProducts.First().CartId);
        }

        [Fact]
        public void AddProductInCartWithInvalidProductShouldNotAddProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInCart_Cart_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var username = "user";
            var user = new User() {UserName = username, Cart = new Cart()};
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(dbContext.Users.FirstOrDefault(x => x.UserName == username));

            var productId = "productId1";
            Product product = null;
            var productService = new Mock<IProductsSerivce>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(product);

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.AddProductToCart(productId, username);

            var cartProducts = dbContext.CartProducts.ToList();

            Assert.Empty(cartProducts);
        }

        [Fact]
        public void AddProductInCartWithExistingProductShouldNotAddProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInCart_Cart_Db")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new User {UserName = "user", Cart = new Cart()};
            dbContext.Users.Add(user);

            var product = new Product {Name = "T-Shirt"};
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            productService.Setup(p => p.GetProductById(product.Id))
                .Returns(product);

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.AddProductToCart(product.Id, user.UserName);

            cartsService.AddProductToCart(product.Id, user.UserName);

            var cartProducts = dbContext.CartProducts.ToList();

            Assert.Single(cartProducts);
        }

        [Fact]
        public void AnyProductsShouldReturnTrueWhenThereAreProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"CartService")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new User {UserName = "user", Cart = new Cart()};
            dbContext.Users.Add(user);

            var product = new Product {Name = "USB Cable"};
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            productService.Setup(p => p.GetProductById(product.Id))
                .Returns(product);

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.AddProductToCart(product.Id, user.UserName);

            var areThereAnyProducts = cartsService.AnyProducts(user.UserName);

            Assert.True(areThereAnyProducts);

        }

        [Fact]
        public void GetAllCartProductsShouldReturnAllCartProductsForUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"CartService")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var products = new List<Product>
            {
                new Product {Name = "Ball 1"},
                new Product {Name = "Ball 2"},
                new Product {Name = "Ball 3"},
                new Product {Name = "Ball 4"}
            };
            dbContext.Products.AddRange(products);

            var cartProducts = new List<CartProduct>
            {
                new CartProduct() {Product = products.First()},
                new CartProduct() {Product = products.Last()},
            };

            var user = new User
            {
                UserName = "user",
                Cart = new Cart
                {
                    Products = cartProducts
                }
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            var result = cartsService.GetAllCartProducts(user.UserName);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllCartProductsWithInvalidUserShouldReturnNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"GetAllShoppingCartProductsNull_Database")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var username = "user@gmail.com";
            User user = null;

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            var cartService = new CartService(dbContext, productService.Object, userService.Object);

            var shoppingCartProducts = cartService.GetAllCartProducts(username);

            Assert.Null(shoppingCartProducts);
        }

        [Fact]
        public void DeleteAllProductFromCartShouldDeleteAllCartProductsForCurrentUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"CartService")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var products = new List<Product>
            {
                new Product { Name = "Ball 1" },
                new Product { Name = "Ball 2" },
                new Product { Name = "Ball 3" },
                new Product { Name = "Ball 4" }
            };
            dbContext.Products.AddRange(products);

            var cartProducts = new List<CartProduct>
            {
                new CartProduct() { Product = products.First() },
                new CartProduct() { Product = products.Last() },
            };

            var user = new User()
            {
                UserName = "user",
                Cart = new Cart
                {
                    Products = cartProducts
                }
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            var cartService = new CartService(dbContext, productService.Object, userService.Object);

            cartService.DeleteAllProductsFromCart(user.UserName);

            var userShoppingCart = dbContext.CartProducts.Where(x => x.CartId == user.CartId);

            Assert.Empty(userShoppingCart);
        }


        [Fact]
        public void DeleteProductFromCartShoulDeleteExactProductFromCurrentUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: $"CartService")
             .Options;
            var dbContext = new ApplicationDbContext(options);

            var products = new List<Product>
            {
               new Product { Name = "Ball 1" },
               new Product { Name = "Ball 2" },
               new Product { Name = "Ball 3" },
               new Product { Name = "Ball 4" }
            };
            dbContext.Products.AddRange(products);

            var cartProducts = new List<CartProduct>
            {
               new CartProduct { Product = products.First() },
               new CartProduct { Product = products.Last() },
            };

            var user = new User
            {
                UserName = "user",
                Cart = new Cart
                {
                    Products = cartProducts
                }
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(user.UserName))
                       .Returns(user);

            var productForDelete = products.First();
            var productService = new Mock<IProductsSerivce>();
            productService.Setup(p => p.GetProductById(productForDelete.Id))
                          .Returns(productForDelete);

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.DeleteProductFromCart(productForDelete.Id, user.UserName);

            var shoppingCartProduct = dbContext.CartProducts.Where(x => x.CartId == user.CartId && x.ProductId == productForDelete.Id).ToList();

            Assert.Empty(shoppingCartProduct);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(4, 4)]
        public void EditProductQuantityInCartShouldEditProductQuantity(int quantity, int expectedQuantity)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "CartService")
             .Options;
            var dbContext = new ApplicationDbContext(options);

            var product = new Product { Name = "ball 1" };
            dbContext.Products.AddRange(product);

            var cart = new Cart
            {
                Products = new List<CartProduct>
                {
                   new CartProduct() { Product = product, Quantity = 1 },
                }
            };

            var username = "user";
            var user = new User()
            {
                UserName = username,
                Cart = cart
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userService = new Mock<IUsersService>();
            userService.Setup(r => r.GetUserByUsername(username))
                       .Returns(user);

            var productService = new Mock<IProductsSerivce>();
            productService.Setup(p => p.GetProductById(product.Id))
                          .Returns(product);

            var cartsService = new CartService(dbContext, productService.Object, userService.Object);

            cartsService.EditProductInCart(product.Id, username, quantity);

            var shoppingCartProduct = dbContext.CartProducts
                                               .FirstOrDefault(x => x.ProductId == product.Id
                                                     && x.CartId == user.CartId);

            Assert.Equal(expectedQuantity, shoppingCartProduct.Quantity);
        }

    }
}
