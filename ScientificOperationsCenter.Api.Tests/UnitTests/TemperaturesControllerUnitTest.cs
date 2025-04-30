using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Moq;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    internal class TemperaturesControllerUnitTest
    {
        private Mock<ITemperaturesMapper> _temperaturesMapperMock;
        private TemperaturesController _temperaturesController;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            _temperaturesController = new TemperaturesController(_temperaturesMapperMock.Object);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-08";

            // Action
            var result = await _temperaturesController.Day(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(-4));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(contents!.Last().AverageTemperature, Is.EqualTo(30));
                Assert.That(contents!.Count, Is.EqualTo(6));
            });
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
            var result = await _temperaturesController.Month(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("1"));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(32));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("21"));
                Assert.That(contents!.Last().AverageTemperature, Is.EqualTo(20));
                Assert.That(contents!.Count, Is.EqualTo(7));
            });
        }



        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-01-01";

            // Action
            var result = await _temperaturesController.Year(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("May"));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(-2));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("December"));
                Assert.That(contents!.Count, Is.EqualTo(8));
            });
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
            var result = await _temperaturesController.Day(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
            var result = await _temperaturesController.Month(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2025-10-00";

            // Action
            var result = await _temperaturesController.Year(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public void Constructor_WhenTemperaturesMapperIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITemperaturesMapper? temperaturesMapper = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _ = new TemperaturesController(temperaturesMapper!));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGetTemperaturesForTheMonthAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            temperaturesMapperMock.Setup(m => m.GetTemperaturesForTheMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Month"));
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = new DateOnly(2024, 10, _random.Next(1, 30)).ToString();

            // Action
            IActionResult result = await temperaturesController.Month(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenATemperaturesMapper_WhenGetTemperaturesForTheYearAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var temperaturesMapperMock = MockITemperaturesMapper.GetMock();
            temperaturesMapperMock.Setup(m => m.GetTemperaturesForTheYearAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Year"));
            var temperaturesController = new TemperaturesController(temperaturesMapperMock.Object);
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30)).ToString();

            // Action
            IActionResult result = await temperaturesController.Year(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
