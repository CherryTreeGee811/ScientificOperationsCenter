using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests.UnitTests
{
    public class RadiationMeasurementsServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(230));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2024));
            Assert.That(result.First().Date.Month, Is.EqualTo(10));
            Assert.That(result.First().Date.Day, Is.EqualTo(08));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(410));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(new DateOnly(2025, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2025));
            Assert.That(result.First().Date.Month, Is.EqualTo(01));
            Assert.That(result.First().TotalMilligrays, Is.EqualTo(400));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(new DateOnly(2025, 10, 30));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsTimeSum>>(result, "The returned element is not of IEnumerable<RadiationMeasurementsTimeSum> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(new DateOnly(2025, 09, random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateSum>>(result, "The returned element is not of IEnumerable<RadiationMeasurementsDateSum> type.");
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(new DateOnly(2026, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateSum>>(result, "The returned element is not of IEnumerable<RadiationMeasurementsDateSum> type.");
        }


        [Test]
        public void Constructor_WhenRadiationMeasurementsRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IRadiationMeasurementsRepository radiationMeasurementsRepository = null;

            // Action
            TestDelegate action = () => new RadiationMeasurementsService(radiationMeasurementsRepository);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.That(exception.ParamName, Is.EqualTo("radiationMeasurementsRepository"));
        }
    }
}