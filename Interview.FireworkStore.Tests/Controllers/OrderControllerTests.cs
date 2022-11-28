using AutoMapper;
using FluentAssertions;
using Interview.FireworkStore.Core.Domain;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Core.Infrastructure;
using Interview.FireworkStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Runtime.CompilerServices;

namespace Interview.FireworkStore.Controllers.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<ILogger<OrderController>> logger;
        private Mock<IOrderService> orderService;

        private static Order[] GetTestOrders()
        {
            return new[] {
                    new Order()
                    {
                        Id = 1,
                        UserName = "TestUser",
                        FireworkId = 3,
                        Quantity = 1,
                        GroupId = Guid.Empty,

                    },
                   new Order()
                    {
                        Id = 1,
                        UserName = "TestUser",
                        FireworkId = 4,
                        Quantity = 2,
                        GroupId = Guid.Empty,
                    },
                };
        }

        [TestInitialize]
        public void Setup()
        {
            logger = new Mock<ILogger<OrderController>>();
            orderService = new Mock<IOrderService>();
        }

        [TestMethod]
        public void GetTest()
        {
            var userName = "TestUser";
            var amoutOfOrders = 2;
            orderService.Setup(s => s.GetByUser(userName))
                .Returns(GetTestOrders());
            var controller = new OrderController(logger.Object, orderService.Object);
            var result = controller.Get(userName).Result as OkObjectResult;
            Assert.IsNotNull(result);
            var value = (Order[])result.Value;
            Assert.AreEqual(amoutOfOrders, value.Count());
            var order = value.First();
            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.AreEqual(userName, order.UserName);
        }

        [TestMethod]
        public void GetGroupTest()
        {
            var guid = Guid.Empty;
            var amoutOfOrders = 2;
            orderService.Setup(s => s.GetGroupByGuid(guid))
                .Returns(GetTestOrders());
            var controller = new OrderController(logger.Object, orderService.Object);
            var result = controller.GetGroup(Guid.Empty).Result as OkObjectResult;
            Assert.IsNotNull(result);
            var value = (Order[])result.Value;
            Assert.AreEqual(amoutOfOrders, value.Count());
            var order = value.First();
            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.AreEqual(guid, order.GroupId);
        }

        [TestMethod]
        public void Create()
        {
            var order = new OrderDto() 
            {
                UserName = "TestUser",
                FireworkId = 1,
                Quantity = 1,
            };
            orderService.Setup(s => s.Validate(order)).Returns(new ValidationResult());
            orderService.Setup(s => s.Create(order)).Returns(true);
            var controller = new OrderController(logger.Object, orderService.Object);
            var result = controller.Create(order);
            result.Should().BeOfType<CreatedResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }


        [TestMethod]
        public void CreateOrders()
        {
            var orders = new[] {
                    new OrderDto()
                    {
                        UserName = "TestUser",
                        FireworkId = 3,
                        Quantity = 1,

                    },
                   new OrderDto()
                    {
                        UserName = "TestUser",
                        FireworkId = 4,
                        Quantity = 2,
                    },
            };
            orderService.Setup(s => s.Validate(orders)).Returns(new ValidationResult());
            orderService.Setup(s => s.Create(orders)).Returns(true);
            var controller = new OrderController(logger.Object, orderService.Object);
            var result = controller.Create(orders);
            result.Should().BeOfType<CreatedResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }
    }
}