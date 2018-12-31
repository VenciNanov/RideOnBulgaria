namespace RideOnBulgaria.Web.Areas.Shop.Models.Order
{
    public class CurrentUserOrders
    {
        public string Id { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string OrderStatus { get; set; }
    }
}