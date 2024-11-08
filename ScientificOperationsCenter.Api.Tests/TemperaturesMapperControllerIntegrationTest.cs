using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Controllers;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
using ScientificOperationsCenter.Api.ViewModels;
using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
========
using ScientificOperationsCenter.ViewModels;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs


namespace ScientificOperationsCenter.Tests
{
    internal class TemperaturesMapperControllerIntegrationTest
    {
        private Mock<ITemperaturesService> _temperaturesServiceMock;
        private TemperaturesMapper _temperaturesMapper;
        private TemperaturesController _temperaturesController;


        [SetUp]
        public void SetUp()
        {
            _temperaturesServiceMock = MockITemperaturesService.GetMock();
            _temperaturesMapper = new TemperaturesMapper(_temperaturesServiceMock.Object);
            _temperaturesController = new TemperaturesController(_temperaturesMapper);
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            var controllerResult = await _temperaturesController.Day(date);
========
            var controllerResult = temperaturesController.Day(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesTimeViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-1));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
========
            Assert.That(contents.First().Hour, Is.EqualTo(new TimeOnly(1, 00)));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-1));
            Assert.That(contents.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            var controllerResult = await _temperaturesController.Month(date);
========
            var controllerResult = temperaturesController.Month(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(27));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("21"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(15));
========
            Assert.That(contents.First().Date, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(32));
            Assert.That(contents.Last().Date, Is.EqualTo("21"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-01-01";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            var controllerResult = await _temperaturesController.Year(date);
========
            var controllerResult = temperaturesController.Year(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-3));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("December"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(27));
========
            Assert.That(contents.First().Date, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-2));
            Assert.That(contents.Last().Date, Is.EqualTo("December"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesMapperControllerIntegrationTest.cs
            Assert.That(contents.Count, Is.EqualTo(8));
        }
    }
}
