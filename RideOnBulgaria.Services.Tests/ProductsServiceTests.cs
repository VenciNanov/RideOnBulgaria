using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class ProductsServiceTests
    {
        [Fact]
        public void CreateProductShouldReturnTheCreatedProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateProduct_Product_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new Mock<IImageService>();
            imageService.Setup(x => x.AddImageToProduct(It.IsAny<IFormFile>())).Returns(new ProductImage()
            {
                PublicId = "PublicId",
                Id = Convert.ToString(Guid.NewGuid()),
                ImageUrl = "http://www.url.com/",
            });


            var productsService = new ProductsService(dbContext, null, imageService.Object);

            var product = productsService.CreateProduct("Topka", "Kon", 123, GenerateFile(), "Mlqko");

            Assert.IsType<Product>(product);
            Assert.Equal("Topka", product.Name);
        }

        [Fact]
        public void CreateProductShouldNotCreateProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateProduct_Product_Db")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var imageService = new Mock<IImageService>();
            imageService.Setup(x => x.AddImageToProduct(It.IsAny<IFormFile>())).Returns(new ProductImage()
            {
                PublicId = "PublicId",
                Id = Convert.ToString(Guid.NewGuid()),
                ImageUrl = "http://www.url.com/",
            });


            var productsService = new ProductsService(dbContext, null, imageService.Object);

            var product = productsService.CreateProduct(null, "Kon", 123, GenerateFile(), "Mlqko");

            Assert.Null(product);
        }

        [Fact]
        public void GetProductByIdShouldReturnProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProductById_Product_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var productsService = new ProductsService(dbContext, null, null);

            var product = new Product
            {
                Name = "Lorem"
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var result = productsService.GetProductById(product.Id);

            Assert.Equal(product, result);
            Assert.IsType<Product>(result);
        }

        [Fact]
        public void GetAllProductsShouldReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_Product_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var productsService = new ProductsService(dbContext, null, null);

            var products = new List<Product>()
            {
                new Product {Name = "Ball"}, new Product {Name = "Bike"}, new Product {Name = "Bottle"}
            };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            var result = productsService.GetAllProducts();

            Assert.Equal(products, result);
        }

        [Fact]
        public void EditProductShouldReturnTheProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditProduct_Products_Database")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var productsService = new ProductsService(dbContext, null, null);


            var product = new Product
            {
                Name = "Ball",
                Description = "long description",
                IsHidden = false,
                AdditionalInfo = null,
                Price = 99
            };
           

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var result = productsService.EditProduct(product.Id, "NewName", "short", true, "yes", 20);

            Assert.Equal(product.Id, result.Id);
            Assert.NotEqual("Ball", result.Name);
            Assert.True(result.IsHidden);
        }

        [Fact]
        public void EditProductShouldNotEditTheProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditProduct_Products_Db")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var productsService = new ProductsService(dbContext, null, null);


            var product = new Product
            {
                Name = "Ball",
                Description = "long description",
                IsHidden = false,
                AdditionalInfo = null,
                Price = 99
            };


            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var result = productsService.EditProduct("123sddsad", null, "short", true, "yes", 20);

           Assert.Null(result);
        }
        private IFormFile GenerateFile()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            return file;
        }

    }
}