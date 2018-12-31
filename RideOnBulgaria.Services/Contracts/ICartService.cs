using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface ICartService
    {
        IEnumerable<CartProduct> GetAllCartProducts(string username);
        void AddProductToCart(string productId, string username, int? quantity = null);
        void EditProductInCart(string productId, string username, int quantity);
        void DeleteProductFromCart(string id, string username);
        bool AnyProducts(string username);
        void DeleteAllProductsFromCart(string username);
    }
}