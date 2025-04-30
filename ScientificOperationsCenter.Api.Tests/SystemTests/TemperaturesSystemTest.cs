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
    internal class TemperaturesSystemTest
    {
        private MockGroundControlUplinkDownlink _mockGroundControlUplinkDownlink;
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private RadiationMeasurementsRepository _radiationMeasurementsRepository;
        private TemperaturesRepository _temperaturesRepository;
        private TemperaturesService _temperaturesService;
        private TemperaturesMapper _temperaturesMapper;
        private TemperaturesController _temperaturesController;
        private ReceiveController _receiveController;



        [SetUp]
        public async Task SetUp()
        {
            _mockGroundControlUplinkDownlink = new MockGroundControlUplinkDownlink();
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _radiationMeasurementsRepository = new RadiationMeasurementsRepository(_scientificOperationsContext);
            _temperaturesRepository = new TemperaturesRepository(_scientificOperationsContext);
            _temperaturesService = new TemperaturesService(_temperaturesRepository);
            _temperaturesMapper = new TemperaturesMapper(_temperaturesService);
            _temperaturesController = new TemperaturesController(_temperaturesMapper);
            _receiveController = new ReceiveController(_radiationMeasurementsRepository, _temperaturesRepository);

            var temperaturePayloads = _mockGroundControlUplinkDownlink.GetTemperatures();
            foreach (var temperature in temperaturePayloads)
            {
                await _receiveController.Index(temperature);
            }
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByHourOfDay_ThenIf200OKCollectionOfTemperaturesJSONSortedByHourReturn()
        {
            // Setup
            var date = "2024-10-09";

            // Action
            var controllerResult = await _temperaturesController.Day(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<OkObjectResult>());
                var okResult = controllerResult as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo((new TimeOnly(06, 00)).ToString()));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(9));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(contents!.Last().AverageTemperature, Is.EqualTo(11));
                Assert.That(contents!.Count, Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf200OKCollectionOfTemperaturesJSONSortedByDayReturn()
        {
            // Setup
            var date = "2020-10-01";

            // Action
            var controllerResult = await _temperaturesController.Month(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<OkObjectResult>());
                var okResult = controllerResult as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("8"));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(6));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("9"));
                Assert.That(contents!.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(contents!.Count, Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf200OKCollectionOfTemperaturesJSONSortedByMonthReturn()
        {
            // Setup
            var date = "2020-01-01";

            // Action
            var controllerResult = await _temperaturesController.Year(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<OkObjectResult>());
                var okResult = controllerResult as OkObjectResult;
                Assert.That(okResult, Is.Not.Null);
                Assert.That(okResult?.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(okResult?.Value, Is.InstanceOf<IEnumerable<TemperaturesViewModel>>());
                var contents = okResult?.Value as IEnumerable<TemperaturesViewModel>;
                Assert.That(contents, Is.Not.Null);
                Assert.That(contents!.Count, Is.AtLeast(1));
                Assert.That(contents!.First().TimeFrame, Is.EqualTo("September"));
                Assert.That(contents!.First().AverageTemperature, Is.EqualTo(8));
                Assert.That(contents!.Last().TimeFrame, Is.EqualTo("December"));
                Assert.That(contents!.Last().AverageTemperature, Is.EqualTo(5));
                Assert.That(contents!.Count, Is.EqualTo(4));
            });
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByHourOfDay_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _temperaturesController.Day(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<NoContentResult>());
                var noContentResult = controllerResult as NoContentResult;
                Assert.That(noContentResult?.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
            });
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByDayOfMonth_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _temperaturesController.Month(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<NoContentResult>());
                var noContentResult = controllerResult as NoContentResult;
                Assert.That(noContentResult?.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
            });
        }


        [Test]
        public async Task GivenAMockGroundControl_WhenGettingAverageTemperaturesByMonthOfYear_ThenIf204NoContentEmptyCollectionReturn()
        {
            // Setup
            var date = "1900-01-01";

            // Action
            var controllerResult = await _temperaturesController.Year(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(controllerResult, Is.Not.Null);
                Assert.That(controllerResult, Is.InstanceOf<NoContentResult>());
                var noContentResult = controllerResult as NoContentResult;
                Assert.That(noContentResult?.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
            });
        }
    }
}
