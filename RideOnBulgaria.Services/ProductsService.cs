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
        private readonly ImageService imageService;

        public ProductsService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Product CreateProduct(string name, string description, decimal price, int count, IFormFile image, string additionalInfo)
        {
            if (name == null || description == null || price <= 0 || count <= 0) return null;

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

            return product;
        }


    }
}