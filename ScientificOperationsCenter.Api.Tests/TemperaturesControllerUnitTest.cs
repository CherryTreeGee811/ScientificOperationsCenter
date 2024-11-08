using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Controllers;
using ScientificOperationsCenter.Tests.Mocks;
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Moq;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
========
using ScientificOperationsCenter.ViewModels;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs


namespace ScientificOperationsCenter.Tests
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
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-08";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Day(date);
========
            var result = temperaturesController.Day(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-4));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
========
            Assert.That(contents.First().Hour, Is.EqualTo(new TimeOnly(1,00)));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-4));
            Assert.That(contents.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Month(date);
========
            var result = temperaturesController.Month(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(32));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("21"));
========
            Assert.That(contents.First().Date, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(32));
            Assert.That(contents.Last().Date, Is.EqualTo("21"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-01-01";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Year(date);
========
            var result = temperaturesController.Year(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-2));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("December"));
========
            Assert.That(contents.First().Date, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-2));
            Assert.That(contents.Last().Date, Is.EqualTo("December"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(20));
            Assert.That(contents.Count, Is.EqualTo(8));
        }


        [Test]
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByHourOfDay_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Day(date);
            var badRequestResponse = result as StatusCodeResult;
========
            var result = temperaturesController.Day(date);
            var badRequestResult = result as StatusCodeResult;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByDayOfMonth_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Month(date);
========
            var result = temperaturesController.Month(date);
            var badRequestResult = result as StatusCodeResult;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public void GivenATemperaturesMapper_WhenGettingAverageTemperaturesByMonthOfYear_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2025-10-00";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/UnitTests/TemperaturesControllerUnitTest.cs
            var result = await _temperaturesController.Year(date);
========
            var result = temperaturesController.Year(date);
            var badRequestResult = result as StatusCodeResult;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperaturesControllerUnitTest.cs

            // Assert
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
            var date = new DateOnly(2024, 10, _random.Next(1, 30)).ToString();

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
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30)).ToString();

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
