using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class ProductsService : IProductsSerivce
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public ProductsService(ApplicationDbContext context, IMapper mapper, IImageService imageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public Product CreateProduct(string name, string description, decimal price, int count, IFormFile image, string additionalInfo)
        {
            if (name == null || description == null || price <= 0 || count <= 0 || image == null) return null;



            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Count = count,
                AdditionalInfo = additionalInfo,
            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            var img = this.imageService.AddImageToProduct(image);

            img.Product = product;
            img.ProductId = product.Id;

            this.context.ProductImages.Add(img);
            this.context.SaveChanges();

            product.Image = img;
            product.ImageId = img.Id;
            this.context.SaveChanges();
            return product;
        }

        public Product GetProductById(string id)
        {
            var product = this.context.Products.FirstOrDefault(x => x.Id == id);

            return product;
        }

        public T Details<T>(string id)
        {
            var product = this.context.Products.Find(id);

            var model = this.mapper.Map<T>(product);

            return model;
        }

        public ICollection<Product> GetAllProducts()
        {
            var products = this.context.Products.OrderBy(x => x.Price).ToList();

            return products;
        }

        public T IndexProductDetails<T>(string id)
        {
            var product = this.context.Products.Find(id);

            var model = this.mapper.Map<T>(product);

            return model;
        }

        public Product EditProduct(string id, string name, string description, bool isHidden, string additionalInfo,decimal price)
        {
            if (name == null || description == null || price <= 0 ) return null;

            var product = this.GetProductById(id);

            if (product==null)
            {
                return null;
            }

            product.Name = name;
            product.Description = description;
            product.IsHidden = isHidden;
            product.AdditionalInfo = additionalInfo;
            product.Price = price;

            this.context.Update(product);
            this.context.SaveChanges();
            return product;
        }
    }
}