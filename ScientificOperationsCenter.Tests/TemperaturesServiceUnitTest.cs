using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests
{
    public class TemperaturesServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheDayAsync(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            // 08
            var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2024));
            Assert.That(result.First().Date.Month, Is.EqualTo(10));
            Assert.That(result.First().Date.Day, Is.EqualTo(08));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(new DateOnly(2025, random.Next(1,12), random.Next(1,30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2025));
            Assert.That(result.First().Date.Month, Is.EqualTo(01));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheDayAsync(new DateOnly(2025, 10, 30));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeAverage>>(result, "The returned element is not of IEnumerable<TemperaturesTimeAverage> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(new DateOnly(2025, 09, random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateAverage>>(result, "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(new DateOnly(2026, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateAverage>>(result, "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
        }
    }
}