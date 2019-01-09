using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Models.Enums;
using RideOnBulgaria.Services.Contracts;
using Xunit;

namespace RideOnBulgaria.Services.Tests
{
    public class OrdersServiceTests
    {
        [Fact]
        public void CreateOrderShouldCreateOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateOrder_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User { UserName = username };

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.CreateOrder(username);

            var orders = context.Orders.ToList();

            Assert.Single(orders);
        }

        [Fact]
        public void GetProcessingOrderShouldReturnProcessingOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProcessingOrder_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processing},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.CreateOrder(username);

            var order = ordersService.GetProcessingOrder(user.UserName);

            Assert.Equal("id1", order.Id);
        }

        [Fact]
        public void GetOrderByIdShouldReturnTheRightOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetOrderById_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.CreateOrder(username);

            var order = ordersService.GetOrderById("id3");

            Assert.Equal("id3", order.Id);
            Assert.Equal(OrderStatus.Processed, order.OrderStatus);
        }

        [Fact]
        public void GetCurrentUserOrdersShouldReturnTheGiverUserAllOrders()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUserOrders_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
            };

            var orders = new List<Order>
            {
                new Order{OrderStatus = OrderStatus.Delivered,User = user},
                new Order{OrderStatus = OrderStatus.Processed,User=user},
                new Order{OrderStatus = OrderStatus.Sent,User = new User{UserName = "user2"}}
            };
            context.Users.Add(user);
            context.Orders.AddRange(orders);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);

            var result = ordersService.GetCurrentUserOrders(user.UserName);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void DeliverOrderShouldChangeOrderStatusToDeliver()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeliverOrder_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.DeliverOrder("id2");

            var order = context.Orders.FirstOrDefault(x => x.Id == "id2");

            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);
        }

        [Fact]
        public void DeliverOrderShouldNotChangeOrderStatus()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeliverOrder_Orders_Db")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.DeliverOrder("id3");

            var order = context.Orders.FirstOrDefault(x => x.Id == "id3");

            Assert.NotEqual(OrderStatus.Delivered, order.OrderStatus);
        }

        [Fact]
        public void SendOrderShouldChangeOrderStatusToSent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SendOrder_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.SendOrder("id3");

            var order = context.Orders.FirstOrDefault(x => x.Id == "id3");

            Assert.Equal(OrderStatus.Sent, order.OrderStatus);
        }

        [Fact]
        public void SendOrderShouldNotChangeOrderStatus()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SendOrder_Orders_Db")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processing},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);
            ordersService.SendOrder("id1");

            var order = context.Orders.FirstOrDefault(x => x.Id == "id1");

            Assert.NotEqual(OrderStatus.Sent, order.OrderStatus);
        }

        [Fact]
        public void GetProcessedOrdersShouldReturnAllProcessedOrders()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProcessedOrders_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processed},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);


            var orders = ordersService.GetProcessedOrders();

            Assert.Equal(2, orders.Count);

        }

        [Fact]
        public void GetAllOrdersShouldReturnAllOrders()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllOrders_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processed},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);


            var orders = ordersService.GetAllOrders();

            Assert.Equal(3, orders.Count);
        }

        [Fact]
        public void GetSentOrdersShouldReturnAllSentOrders()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSentOrders_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Processed},
                    new Order{Id="id2",OrderStatus = OrderStatus.Sent},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);


            var orders = ordersService.GetSentOrders();

            Assert.Equal(1, orders.Count);
        }

        [Fact]
        public void GetDeliveredOrdersShouldReturnAllDeliveredOrders()
        {


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDeliveredOrders_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    new Order{Id="id1",OrderStatus = OrderStatus.Delivered},
                    new Order{Id="id2",OrderStatus = OrderStatus.Delivered},
                    new Order{Id="id3",OrderStatus = OrderStatus.Processed},
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);


            var orders = ordersService.GetDeliveredOrders();

            Assert.Equal(2, orders.Count);
        }

        [Fact]
        public void GetOrderDetailsShouldReturnOrderDetailsForTheGivenId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetOrderDetails_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var orderProducts = new List<OrderProduct>()
            {
                new OrderProduct
                {
                    OrderId = "order1",
                    ProductId = "product1",
                    Quantity = 2,
                },
                new OrderProduct
                {
                    OrderId = "order1",
                    ProductId = "product2",
                    Quantity = 3,
                },
                new OrderProduct
                {
                    OrderId = "order2",
                    ProductId = "product1",
                    Quantity = 1,
                },
            };

            context.OrderProducts.AddRange(orderProducts);
            context.SaveChanges();


            var ordersService = new OrdersService(context, null, cartService.Object);


            var order = ordersService.GetOrderDetails("order1");

            Assert.Equal(2, order.Count);
        }

        [Fact]
        public void GetOrderDetailsShouldNotReturnOrderDetailsForTheGivenId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetOrderDetails_Orders_Db")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();

            var orderProducts = new List<OrderProduct>()
            {
                new OrderProduct
                {
                    OrderId = "order1",
                    ProductId = "product1",
                    Quantity = 2,
                },
                new OrderProduct
                {
                    OrderId = "order1",
                    ProductId = "product2",
                    Quantity = 3,
                },
                new OrderProduct
                {
                    OrderId = "order2",
                    ProductId = "product1",
                    Quantity = 1,
                },
            };

            context.OrderProducts.AddRange(orderProducts);
            context.SaveChanges();


            var ordersService = new OrdersService(context, null, cartService.Object);


            var order = ordersService.GetOrderDetails("order3");

            Assert.Null(order);
        }

        [Fact]
        public void MakeOrderShouldSetOrderDetails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MakeOrder_Orders_Database")
                .Options;
            var context = new ApplicationDbContext(options);

            var cartService = new Mock<ICartService>();


            var order = new Order { Id = "id1", OrderStatus = OrderStatus.Processing };

            var username = "user";
            var user = new User
            {
                UserName = username,
                Orders = new List<Order>
                {
                    order
                }
            };

            context.Users.Add(user);
            context.SaveChanges();

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(username))
                .Returns(user);

            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);


            ordersService.MakeOrder(order, "Stamat Stamatov", "0123456789", "Box", "UnderTheBridge", "nope");

            Assert.Equal("Stamat Stamatov", order.RecipientName);
            Assert.Equal("0123456789", order.RecipientPhoneNumber);
            Assert.Equal("Box", order.Address);
            Assert.Equal("UnderTheBridge", order.City);
        }

        [Theory]
        [InlineData(15, 1)]
        [InlineData(30, 2)]
        [InlineData(45, 3)]
        public void CompleteOrderShouldCompleteOrderAndChangeStatusToProcessed(decimal totalPrice, int quantity)
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "CompleteOrder_Orders_Database")
            .Options;
            var context = new ApplicationDbContext(options);

            var user = new User
            {
                UserName = "User1",
                Orders = new List<Order> { new Order() { OrderStatus = OrderStatus.Processing } },
                Cart = new Cart()
            };

            var cartProducts = new List<CartProduct>
            {
                new CartProduct
                {
                    Product = new Product {Name = "Tshirt", Price = 10},
                    Cart = user.Cart,
                    Quantity = quantity
                },
                new CartProduct
                {
                    Product = new Product {Name = "Ball", Price = 5},
                    Cart = user.Cart,
                    Quantity = quantity
                },
            };

            context.Users.Add(user);
            context.CartProducts.AddRange(cartProducts);
            context.SaveChanges();

            var cartService = new Mock<ICartService>();
            cartService.Setup(x => x.GetAllCartProducts(user.UserName)).Returns(cartProducts);

            var usersService = new Mock<IUsersService>();
            usersService.Setup(x => x.GetUserByUsername(user.UserName))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == user.UserName));
            var ordersService = new OrdersService(context, usersService.Object, cartService.Object);

            ordersService.CompleteOrder(user.UserName);

            var order = context.Orders.FirstOrDefault(x => x.User.UserName == user.UserName);

            Assert.Equal(OrderStatus.Processed, order.OrderStatus);
            Assert.Equal(2, order.OrderProducts.Count);
            Assert.Equal(totalPrice, order.TotalPrice);

        }
    }
}