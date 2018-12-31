using System;
using System.Collections.Generic;
using RideOnBulgaria.Models.Enums;

namespace RideOnBulgaria.Models
{
    public class Order
    {
        public string Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string UserId { get; set; }

        public string AdditionalInformation { get; set; }

        public string RecipientPhoneNumber { get; set; }
        public string RecipientName { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts{ get; set; }
    }
}