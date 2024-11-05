using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;
using Moq;
using ScientificOperationsCenter.Api.DAL.Interfaces;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    class RadiationMeasurementsServiceMapperIntegrationTest
    {

        private Mock<IRadiationMeasurementsRepository> _radiationMeasurementsRepositoryMock;
        private RadiationMeasurementsService _radiationMeasurementsService;
        private RadiationMeasurementsMapper _radiationMeasurementsMapper;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            _radiationMeasurementsService = new RadiationMeasurementsService(_radiationMeasurementsRepositoryMock.Object);
            _radiationMeasurementsMapper = new RadiationMeasurementsMapper(_radiationMeasurementsService);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsTimeViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo((new TimeOnly(06, 00)).ToString()));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(280));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(230));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo("8"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo("9"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(510));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo("October"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(920));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo("November"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(300));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 10, 01);

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2024, 09, _random.Next(1, 30));

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2026, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsViewModel> type.");
        }
    }
}
