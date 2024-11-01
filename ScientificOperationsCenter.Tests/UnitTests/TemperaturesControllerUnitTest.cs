using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Moq;


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
            var date = "2024-10-01";

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
            var date = "2025-01-01";

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


        [Test]
        public void Constructor_WhenTemperaturesMapperIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITemperaturesMapper temperaturesMapper = null;

            // Action
            TestDelegate action = () => new TemperaturesController(temperaturesMapper);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.That(exception.ParamName, Is.EqualTo("temperaturesMapper"));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGetTemperaturesForTheDayAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            temperaturesMapperMock.Setup(m => m.GetTemperaturesForTheDayAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Day"));
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = "2025-10-08";

            // Action
            IActionResult result = await temperaturesController.Day(date);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGetTemperaturesForTheMonthAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            temperaturesMapperMock.Setup(m => m.GetTemperaturesForTheMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Month"));
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30)).ToString();

            // Action
            IActionResult result = await temperaturesController.Month(date);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGetTemperaturesForTheYearAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            temperaturesMapperMock.Setup(m => m.GetTemperaturesForTheYearAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Year"));
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30)).ToString();

            // Action
            IActionResult result = await temperaturesController.Year(date);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
