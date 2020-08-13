using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;
using ScrapIt.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ScrapIt.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            //Arrange
            var mockService = new Mock<IWebScrapperService>();
            var mockLogger = new Mock<ILogger<WebScrapperController>> ();
            mockService.Setup(s => s.Get(2)).Returns(GetTestCars(2));
            var controller = new WebScrapperController(mockLogger.Object, mockService.Object);

            //Act
            var result = await controller.Get(2);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CarDto>>(actionResult.Value);
            Assert.Equal((await GetTestCars(2)).Count, model.Count());
        }

        private Task<List<CarDto>> GetTestCars(int taskId)
        {
            var cars = new List<CarDto>()
            {
                new CarDto { Name = "Test auto",
                             Description = "Test description",
                             Price = 100001,
                             TaskId = 2,
                             PublishDate =  DateTime.Parse("01.01.2020"),
                             Url = "www.testrrl1"
                            },
                new CarDto { Name = "Test auto2",
                             Description = "Test description2",
                             Price = 100002,
                             TaskId = 2,
                             PublishDate =  DateTime.Parse("02.01.2020"),
                             Url = "www.testrrl2"
                            },
            };

            return Task.Run(()=> cars);
        }
    }
}
