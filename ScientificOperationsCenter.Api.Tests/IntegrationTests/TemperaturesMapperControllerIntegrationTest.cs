using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    internal class TemperaturesMapperControllerIntegrationTest
    {
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private TemperaturesRepository _temperaturesRepository;
        private Mock<ITemperaturesService> _temperaturesServiceMock;
        private TemperaturesMapper _temperaturesMapper;
        private TemperaturesController _temperaturesController;


        [SetUp]
        public void SetUp()
        {
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _temperaturesRepository = new TemperaturesRepository(_scientificOperationsContext);
            _temperaturesServiceMock = MockITemperaturesService.GetMock();
            _temperaturesMapper = new TemperaturesMapper(_temperaturesServiceMock.Object);
            _temperaturesController = new TemperaturesController(_temperaturesMapper, _temperaturesRepository);
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200CollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
            var controllerResult = await _temperaturesController.Day(date);
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
            Assert.That(contents.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-1));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(contents.Count, Is.EqualTo(6));
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200CollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
            var controllerResult = await _temperaturesController.Month(date);
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
            Assert.That(contents.First().TimeFrame, Is.EqualTo("1"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(27));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("21"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(15));
            Assert.That(contents.Count, Is.EqualTo(7));
        }



        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200CollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2025-01-01";

            // Action
            var controllerResult = await _temperaturesController.Year(date);
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
            Assert.That(contents.First().TimeFrame, Is.EqualTo("May"));
            Assert.That(contents.First().AverageTemperature, Is.EqualTo(-3));
            Assert.That(contents.Last().TimeFrame, Is.EqualTo("December"));
            Assert.That(contents.Last().AverageTemperature, Is.EqualTo(27));
            Assert.That(contents.Count, Is.EqualTo(8));
        }
    }
}
