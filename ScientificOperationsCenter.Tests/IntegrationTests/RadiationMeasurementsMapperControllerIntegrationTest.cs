using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    internal class RadiationMeasurementsMapperControllerIntegrationTest
    {
        [Test]
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByHourOfDay_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapper);
            var date = "2024-10-09";

            // Action
            var controllerResult = await radiationMeasurementsController.Day(date);
            var okResult = controllerResult as ObjectResult;

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
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByDayOfMonth_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapper);
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
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByMonthOfYear_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var radiationMeasurementsController = new RadiationMeasurementsController(radiationMeasurementsMapper);
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
    }
}
