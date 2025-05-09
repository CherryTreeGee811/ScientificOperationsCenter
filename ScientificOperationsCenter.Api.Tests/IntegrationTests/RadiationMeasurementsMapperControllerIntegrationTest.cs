﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Api.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using Moq;


namespace ScientificOperationsCenter.Api.Tests.IntegrationTests
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
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByHourOfDay_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
            var controllerResult = await _radiationMeasurementsController.Day(date);
            var okResult = controllerResult as ObjectResult;

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
                Assert.That(contents!.First().TimeFrame, Is.EqualTo((new TimeOnly(01, 00)).ToString()));
                Assert.That(contents!.First().TotalRadiation, Is.EqualTo(430));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(contents!.Last().TotalRadiation, Is.EqualTo(110));
                Assert.That(contents!.Count, Is.EqualTo(6));
            });
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByDayOfMonth_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-20";

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
        public async Task GivenARadiationMeasurementsService_WhenRadiationMeasurementsSumByMonthOfYear_ThenIf200CollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-10-01";

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
    }
}
