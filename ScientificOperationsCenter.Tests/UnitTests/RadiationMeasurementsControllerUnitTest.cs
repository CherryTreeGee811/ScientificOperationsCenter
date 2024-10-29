using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.UnitTests
{
    internal class RadiationMeasurementsControllerUnitTest
    {
        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByHourOfDay_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2024-10-08";

            // Action
            var result = await radiationMeasurementsController.Day(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsTimeViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsTimeViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Hour, Is.EqualTo(new TimeOnly(1, 00)));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(contents.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByDayOfMonth_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2024-10-20";

            // Action
            var result = await radiationMeasurementsController.Month(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Date, Is.EqualTo("1"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().Date, Is.EqualTo("21"));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenRadiationMeasurementsSumByMonthOfYear_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2025-10-01";

            // Action
            var result = await radiationMeasurementsController.Year(date);
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.NotNull(okResult.Value);
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsDateViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Date, Is.EqualTo("May"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().Date, Is.EqualTo("December"));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(150));
            Assert.That(contents.Count, Is.EqualTo(8));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByHourOfDay_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2024-10-00";

            // Action
            var result = await radiationMeasurementsController.Day(date);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByDayOfMonth_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2024-10-00";

            // Action
            var result = await radiationMeasurementsController.Month(date);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }


        [Test]
        public async Task GivenARadiationMeasurementsMapper_WhenGettingRadiationMeasurementsSumByMonthOfYear_ThenIfBadRequest400ResponseCodeReturn()
        {
            // Setup
            var radiationMeasurementsMapperMock = MockIRadiationMeasurementsMapper.GetMock();
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapperMock.Object);
            var date = "2025-10-00";

            // Action
            var result = await radiationMeasurementsController.Year(date);
            var badRequestResponse = result as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}
