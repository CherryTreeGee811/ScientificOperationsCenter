using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Tests
{
    class RadiationMeasurementsServiceMapperIntegrationTest
    {

        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsTimeViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var date = new DateOnly(2024, 10, 09);

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Hour, Is.EqualTo(new TimeOnly(6, 00)));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(280));
            Assert.That(mapperResult.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(230));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("8"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("9"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(510));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("October"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(920));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("November"));
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(300));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2025, 10, 01);

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsTimeViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsTimeViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2024, 09, random.Next(1, 30));

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsService);
            var random = new Random();
            var date = new DateOnly(2026, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }
    }
}
