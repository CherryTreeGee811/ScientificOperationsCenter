using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.IntegrationTests
{
    internal class RadiationMeasurementsRepositoryServiceIntegrationTest
    {
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private RadiationMeasurementsRepository _radiationMeasurementsRepository;
        private RadiationMeasurementsService _radiationMeasurementsService;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _radiationMeasurementsRepository = new RadiationMeasurementsRepository(_scientificOperationsContext);
            _radiationMeasurementsService = new RadiationMeasurementsService(_radiationMeasurementsRepository);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var serviceResult = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(date);

            // Assert
            Assert.NotNull(serviceResult);
            Assert.That(serviceResult.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(serviceResult.First().TotalMilligrays, Is.EqualTo(230));
            Assert.That(serviceResult.Last().Time, Is.EqualTo(new TimeOnly(06, 00)));
            Assert.That(serviceResult.Last().TotalMilligrays, Is.EqualTo(160));
            Assert.That(serviceResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumDayRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var serviceResult = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(date);

            // Assert
            Assert.NotNull(serviceResult);
            Assert.That(serviceResult.First().Date, Is.EqualTo(new DateOnly(2024, 10, 08)));
            Assert.That(serviceResult.First().TotalMilligrays, Is.EqualTo(410));
            Assert.That(serviceResult.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
            Assert.That(serviceResult.Last().TotalMilligrays, Is.EqualTo(390));
            Assert.That(serviceResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumMontRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2025, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var serviceResult = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(date);

            // Assert
            Assert.NotNull(serviceResult);
            Assert.That(serviceResult.First().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
            Assert.That(serviceResult.First().TotalMilligrays, Is.EqualTo(400));
            Assert.That(serviceResult.Last().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
            Assert.That(serviceResult.Last().TotalMilligrays, Is.EqualTo(400));
            Assert.That(serviceResult.Count(), Is.EqualTo(1));
        }
    }
}
