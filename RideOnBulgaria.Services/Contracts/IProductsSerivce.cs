using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IProductsSerivce
    {
        Product CreateProduct(string name, string description, decimal price, int count, IFormFile image,
            string additionalInfo);

        T Details<T>(string id);

        ICollection<Product> GetAllProducts();
        T IndexProductDetails<T>(string id);
        Product GetProductById(string id);

    }
}