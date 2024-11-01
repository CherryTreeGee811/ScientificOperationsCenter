using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    internal class TemperaturesMapperControllerIntegrationTest
    {
        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var temperaturesController = new TemperaturesController(temperaturesMapper);
            var date = "2024-10-09";

            // Action
            var controllerResult = await temperaturesController.Day(date);
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-1));
            Assert.That(contents.Last().Timeframe, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var temperaturesController = new TemperaturesController(temperaturesMapper);
            var date = "2024-10-01";

            // Action
            var controllerResult = await temperaturesController.Month(date);
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(27));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("21"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(15));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var temperaturesController = new TemperaturesController(temperaturesMapper);
            var date = "2025-01-01";

            // Action
            var controllerResult = await temperaturesController.Year(date);
            var okResult = controllerResult as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-3));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("December"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(27));
            Assert.That(contents.Count, Is.EqualTo(8));
        }
    }
}
