using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.IntegrationTests
{
    internal class TemperaturesRepositoryServiceIntegrationTest
    {
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private TemperaturesRepository _temperaturesRepository;
        private TemperaturesService _temperaturesService;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _temperaturesRepository = new TemperaturesRepository(_scientificOperationsContext);
            _temperaturesService = new TemperaturesService(_temperaturesRepository);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourlyTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(11));
                Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(06, 00)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(9));
                Assert.That(result.Count(), Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDailyTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 10, 08)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(6));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Count(), Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthlyTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2025, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Count(), Is.EqualTo(1));
            });
        }
    }
}
