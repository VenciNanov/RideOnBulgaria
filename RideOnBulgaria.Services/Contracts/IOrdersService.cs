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
        void SendOrder(string id);
        void DeliverOrder(string id);
        List<OrderProduct> GetOrderDetails(string id);
        List<Order> GetAllOrders();
        List<Order> GetProcessedOrders();
        List<Order> GetSentOrders();
        List<Order> GetDeliveredOrders();
        Order GetOrderById(string id);
    }
}