using System.Collections.Generic;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IOrdersService
    {
        Order CreateOrder(string username);
        Order GetProcessingOrder(string username);

        void MakeOrder(Order order, string fullname, string phoneNumber, string address, string city,
            string additionalInformation);

        List<Order> GetCurrentUserOrders(string username);
        void CompleteOrder(string username);
    }
}