using AutoMapper;
using Interview.FireworkStore.Core.Domain;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Interview.FireworkStore.Controllers.Tests
{
    [TestClass]
    public class FireworkControllerTests
    {
        private Mock<ILogger<FireworkController>> logger;
        private Mock<IFireworkService> fireworkService;

        private static Firework[] GetFireworks()
        {
            return new[] {
                    new Firework()
                    {
                        Id = 1,
                        Name = "TestFirework1",
                        Quantity = 3
                    },
                    new Firework()
                    {
                        Id = 2,
                        Name = "TestFirework2",
                        Quantity = 4
                    }
                };
        }

        [TestInitialize]
        public void Setup()
        {
            logger = new Mock<ILogger<FireworkController>>();
            fireworkService = new Mock<IFireworkService>();
        }

        [TestMethod]
        public void GetAllTest()
        {
            var id = 1;
            var quantity = 3;
            var name = "TestFirework1";

            fireworkService.Setup(s => s.GetAll())
                .Returns(GetFireworks());

            var config = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            var controller = new FireworkController(logger.Object, fireworkService.Object, mapper);

            var result = controller.Get().Result as OkObjectResult;
            Assert.IsNotNull(result);
            var value = ((IEnumerable<FireworkDto>)result.Value).First();

            Assert.IsInstanceOfType(value, typeof(FireworkDto));
            Assert.AreEqual(name, value.Name);
            Assert.AreEqual(id, value.Id);
            Assert.AreEqual(quantity, value.Quantity);
        }
    }
}