namespace RideOnBulgaria.Web.Areas.Shop.Models
{
    public class CartProductsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }
    }
}