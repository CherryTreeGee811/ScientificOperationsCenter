using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Controllers;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using Moq;
========
using ScientificOperationsCenter.ViewModels;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs


namespace ScientificOperationsCenter.Tests
{
    internal class RadiationMeasurementsMapperControllerIntegrationTest
    {

        private Mock<IRadiationMeasurementsService> _radiationMeasurementsServiceMock;
        private RadiationMeasurementsMapper _radiationMeasurementsMapper;
        private RadiationMeasurementsController _radiationMeasurementsController;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            _radiationMeasurementsMapper = new RadiationMeasurementsMapper(_radiationMeasurementsServiceMock.Object);
            _radiationMeasurementsController = new RadiationMeasurementsController(_radiationMeasurementsMapper);
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByHourOfDay_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            var controllerResult = await _radiationMeasurementsController.Day(date);
========
            var controllerResult = radiationMeasurementsController.Day(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo((new TimeOnly(01, 00)).ToString()));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
========
            Assert.That(contents.First().Hour, Is.EqualTo(new TimeOnly(1, 00)));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(contents.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByDayOfMonth_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-20";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            var result = await _radiationMeasurementsController.Month(date);
========
            var result = radiationMeasurementsController.Month(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("1"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("21"));
========
            Assert.That(contents.First().Date, Is.EqualTo("1"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().Date, Is.EqualTo("21"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public void GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByMonthOfYear_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-10-01";

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            var result = await _radiationMeasurementsController.Year(date);
========
            var result = radiationMeasurementsController.Year(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
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
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.First().TimeFrame, Is.EqualTo("May"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("December"));
========
            Assert.That(contents.First().Date, Is.EqualTo("May"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(contents.Last().Date, Is.EqualTo("December"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsMapperControllerIntegrationTest.cs
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(150));
            Assert.That(contents.Count, Is.EqualTo(8));
        }
    }
}
