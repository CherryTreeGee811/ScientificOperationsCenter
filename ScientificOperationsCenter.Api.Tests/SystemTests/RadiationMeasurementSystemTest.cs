using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.Controllers;
using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Api.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Tests.SystemTests
{
    internal class RadiationMeasurementSystemTest
    {
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private RadiationMeasurementsRepository _radiationMeasurementsRepository;
        private RadiationMeasurementsService _radiationMeasurementsService;
        private RadiationMeasurementsMapper _radiationMeasurementsMapper;
        private RadiationMeasurementsController _radiationMeasurementsController;


        [SetUp]
        public void SetUp()
        {
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _radiationMeasurementsRepository = new RadiationMeasurementsRepository(_scientificOperationsContext);
            _radiationMeasurementsService = new RadiationMeasurementsService(_radiationMeasurementsRepository);
            _radiationMeasurementsMapper = new RadiationMeasurementsMapper(_radiationMeasurementsService);
            _radiationMeasurementsController = new RadiationMeasurementsController(_radiationMeasurementsMapper);
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByHourOfDay_ThenIf200OKCollectionOfRadiationMeasurementsJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
            var controllerResult = await _radiationMeasurementsController.Day(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<OkObjectResult>(controllerResult);
            var okResult = controllerResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo((new TimeOnly(06, 00)).ToString()));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(160));
            Assert.That(contents.Last().Timeframe, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(230));
            Assert.That(contents.Count, Is.EqualTo(2));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByDayOfMonth_ThenIf200OKCollectionOfRadiationMeasurementsJSONSortedByDayReturn()
        {
            // Setup
            var date = "2024-10-01";

            // Action
            var controllerResult = await _radiationMeasurementsController.Month(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<OkObjectResult>(controllerResult);
            var okResult = controllerResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("8"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("9"));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(390));
            Assert.That(contents.Count, Is.EqualTo(2));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByMonthOfYear_ThenIf200OKCollectionOfRadiationMeasurementsJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2024-01-01";

            // Action
            var controllerResult = await _radiationMeasurementsController.Year(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<OkObjectResult>(controllerResult);
            var okResult = controllerResult as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(okResult.Value);
            var contents = okResult.Value as IEnumerable<RadiationMeasurementsViewModel>;
            Assert.NotNull(contents);
            Assert.That(contents.Count, Is.AtLeast(1));
            Assert.That(contents.First().Timeframe, Is.EqualTo("September"));
            Assert.That(contents.First().TotalRadiation, Is.EqualTo(402));
            Assert.That(contents.Last().Timeframe, Is.EqualTo("December"));
            Assert.That(contents.Last().TotalRadiation, Is.EqualTo(378));
            Assert.That(contents.Count, Is.EqualTo(4));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByHourOfDay_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _radiationMeasurementsController.Day(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<NoContentResult>(controllerResult);
            var noContentResult = controllerResult as NoContentResult;
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByDayOfMonth_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _radiationMeasurementsController.Month(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<NoContentResult>(controllerResult);
            var noContentResult = controllerResult as NoContentResult;
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsByMonthOfYear_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _radiationMeasurementsController.Year(date);

            // Assert
            Assert.NotNull(controllerResult);
            Assert.IsInstanceOf<NoContentResult>(controllerResult);
            var noContentResult = controllerResult as NoContentResult;
            Assert.That(noContentResult.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
        }
    }
}
