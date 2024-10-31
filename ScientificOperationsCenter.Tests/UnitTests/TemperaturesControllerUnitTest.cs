using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.UnitTests
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
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-4));
            Assert.That(contents.Last().Timeframe, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
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
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(32));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("21"));
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
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<TemperaturesViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-2));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("December"));
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
            var badRequestResponse = result as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
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

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
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

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}
