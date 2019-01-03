namespace RideOnBulgaria.Web.Areas.Administration.Models.Users
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal PriceForOne { get; set; }

        public decimal Total { get; set; }
    }
}