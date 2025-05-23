﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Moq;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    internal class RadiationMeasurementsControllerUnitTest
    {
        private Mock<IRadiationMeasurementsMapper> _radiationMeasurementsMapper;
        private RadiationMeasurementsController _radiationMeasurementsController;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsMapper = MockIRadiationMeasurementsMapper.GetMock();
            _radiationMeasurementsController = new RadiationMeasurementsController(_radiationMeasurementsMapper.Object);
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByHourOfDay_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-08";

            // Action
            var result = await _radiationMeasurementsController.Day(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<RadiationMeasurementsViewModel>>());
                var contents = okResult?.Value as IEnumerable<RadiationMeasurementsViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
                Assert.That(contents!.First().TotalRadiation, Is.EqualTo(430));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(contents!.Last().TotalRadiation, Is.EqualTo(110));
                Assert.That(contents!.Count, Is.EqualTo(6));
            });
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByDayOfMonth_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
            var result = await _radiationMeasurementsController.Month(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<RadiationMeasurementsViewModel>>());
                var contents = okResult?.Value as IEnumerable<RadiationMeasurementsViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("1"));
                Assert.That(contents!.First().TotalRadiation, Is.EqualTo(120));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("21"));
                Assert.That(contents!.Last().TotalRadiation, Is.EqualTo(110));
                Assert.That(contents!.Count, Is.EqualTo(7));
            });
        }



        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByMonthOfYear_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-01-01";

            // Action
            var result = await _radiationMeasurementsController.Year(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult, Is.InstanceOf<OkObjectResult>());
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.Not.Null);
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<RadiationMeasurementsViewModel>>());
                var contents = okResult?.Value as IEnumerable<RadiationMeasurementsViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("May"));
                Assert.That(contents!.First().TotalRadiation, Is.EqualTo(120));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("December"));
                Assert.That(contents!.Last().TotalRadiation, Is.EqualTo(150));
                Assert.That(contents!.Count, Is.EqualTo(8));
            });
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByHourOfDay_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
            var result = await _radiationMeasurementsController.Day(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByDayOfMonth_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2024-10-00";

            // Action
            var result = await _radiationMeasurementsController.Month(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByMonthOfYear_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var date = "2025-10-00";

            // Action
            var result = await _radiationMeasurementsController.Year(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public void Constructor_WhenRadiationMeasurementsMapperIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IRadiationMeasurementsMapper? radiationMeasurementsMapper = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _ = new RadiationMeasurementsController(radiationMeasurementsMapper!));
            Assert.That(exception.ParamName, Is.EqualTo("radiationMeasurementsMapper"));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGetRadiationMeasurementsForTheDayAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            radiationMeasurementsMapperMock.Setup(m => m.GetRadiationMeasurementsForTheDayAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Day"));
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2025-10-01";

            // Action
            IActionResult result = await radiationMeasurementsController.Day(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGetRadiationMeasurementsForTheMonthAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            radiationMeasurementsMapperMock.Setup(m => m.GetRadiationMeasurementsForTheMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Month"));
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2025-10-01";

            // Action
            IActionResult result = await radiationMeasurementsController.Month(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGetRadiationMeasurementsForTheYearAsync_ThenIfServerError500ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            radiationMeasurementsMapperMock.Setup(m => m.GetRadiationMeasurementsForTheYearAsync(It.IsAny<DateOnly>()))
                .Throws(new Exception("Test exception handling for Api endpoint Year"));
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2025-10-01";

            // Action
            IActionResult result = await radiationMeasurementsController.Year(date);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            var internalErrorResult = result as StatusCodeResult;
            Assert.That(internalErrorResult?.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
