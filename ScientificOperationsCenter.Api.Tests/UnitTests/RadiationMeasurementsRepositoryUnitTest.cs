using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    internal class RadiationMeasurementsRepositoryUnitTest
    {
        private ScientificOperationsCenterContext _scientificOperationsContext;
        private RadiationMeasurementsRepository _radiationMeasurementsRepository;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _scientificOperationsContext = MockScientificOperationsCenterContext.GetMock();
            _radiationMeasurementsRepository = new RadiationMeasurementsRepository(_scientificOperationsContext);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
            _scientificOperationsContext?.Database.EnsureDeleted();
            _scientificOperationsContext?.Dispose();
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsDay_ThenIfAnyForDayRadiationMeasurementsFilterAndReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _radiationMeasurementsRepository.GetByDayAsync(date);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Id, Is.EqualTo(7));
            Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 30)));
            Assert.That(result.First().Milligrays, Is.EqualTo(120));
            Assert.That(result.Last().Id, Is.EqualTo(9));
            Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
            Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(06, 00)));
            Assert.That(result.Last().Milligrays, Is.EqualTo(160));
            Assert.That(result.Count(), Is.EqualTo(3));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsMonth_ThenIfAnyForMonthRadiationMeasurementsFilterAndReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsRepository.GetByMonthAsync(date);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Id, Is.EqualTo(4));
            Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 10, 08)));
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(16, 00)));
            Assert.That(result.First().Milligrays, Is.EqualTo(100));
            Assert.That(result.Last().Id, Is.EqualTo(9));
            Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
            Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(06, 00)));
            Assert.That(result.Last().Milligrays, Is.EqualTo(160));
            Assert.That(result.Count(), Is.EqualTo(6));
        }


        [Test]
        public async Task GivenAMockContext_WhenGettingTotalRadiationMeasurementsYear_ThenIfAnyForYearRadiationMeasurementsFilterAndReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsRepository.GetByYearAsync(date);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Id, Is.EqualTo(1));
            Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 09, 08)));
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(16, 00)));
            Assert.That(result.First().Milligrays, Is.EqualTo(100));
            Assert.That(result.Last().Id, Is.EqualTo(14));
            Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 12, 05)));
            Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(07, 30)));
            Assert.That(result.Last().Milligrays, Is.EqualTo(126));
            Assert.That(result.Count(), Is.EqualTo(14));
        }
    }
}
