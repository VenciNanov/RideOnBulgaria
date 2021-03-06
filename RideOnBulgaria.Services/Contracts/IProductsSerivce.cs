﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IProductsSerivce
    {
        Product CreateProduct(string name, string description, decimal price, IFormFile image,
            string additionalInfo);

        T Details<T>(string id);

        ICollection<Product> GetAllProducts();
        T IndexProductDetails<T>(string id);
        Product GetProductById(string id);

        Product EditProduct(string id, string name, string description, bool isHidden, string additionalInfo,
            decimal price);

    }
}