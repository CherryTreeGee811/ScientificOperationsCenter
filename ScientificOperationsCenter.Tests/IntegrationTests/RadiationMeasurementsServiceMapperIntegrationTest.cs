using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    class RadiationMeasurementsServiceMapperIntegrationTest
    {

        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsTimeViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var date = new DateOnly(2024, 10, 09);

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Hour, Is.EqualTo(new TimeOnly(6, 00)));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(280));
            Assert.That(mapperResult.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(230));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("8"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("9"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(510));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("October"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(920));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("November"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(300));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2025, 10, 01);

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsTimeViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsTimeViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, 09, random.Next(1, 30));

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2026, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }
    }
}
