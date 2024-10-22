using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Controllers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Tests
{
    internal class TemperaturesControllerUnitTest
    {
        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2024-10-08";

            // Action
            var result = await temperaturesController.Day(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesTimeViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Hour, Is.EqualTo(new TimeOnly(1,00)));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-4));
            Assert.That(contents.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2024-10-20";

            // Action
            var result = await temperaturesController.Month(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Date, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(32));
            Assert.That(contents.Last().Date, Is.EqualTo("21"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2025-10-01";

            // Action
            var result = await temperaturesController.Year(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Date, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-2));
            Assert.That(contents.Last().Date, Is.EqualTo("December"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
            Assert.That(contents.Count, Is.EqualTo(8));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2024-10-00";

            // Action
            var result = await temperaturesController.Day(date);
            var badRequestResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2024-10-00";

            // Action
            var result = await temperaturesController.Month(date);
            var badRequestResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2025-10-00";

            // Action
            var result = await temperaturesController.Year(date);
            var badRequestResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}
