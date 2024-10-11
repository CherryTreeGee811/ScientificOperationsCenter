using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests
{
    public class RadiationMeasurementsServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);

            // Action
            var result = radiationMeasurementsService.GetRadiationMeasurementsSumForTheDay(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(230));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonth(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2024));
            Assert.That(result.First().Date.Month, Is.EqualTo(10));
            Assert.That(result.First().Date.Day, Is.EqualTo(08));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(410));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = radiationMeasurementsService.GetRadiationMeasurementsSumForTheYear(new DateOnly(2025, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2025));
            Assert.That(result.First().Date.Month, Is.EqualTo(01));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(400));
            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}