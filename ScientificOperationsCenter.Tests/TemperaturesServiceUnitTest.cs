using ScientificOperationsCenter.BusinessLogic;
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
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);

            // Action
            var result = temperaturesService.GetAverageTemperaturesForTheDay(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            // 08
            var result = temperaturesService.GetAverageTemperaturesForTheMonth(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2024));
            Assert.That(result.First().Date.Month, Is.EqualTo(10));
            Assert.That(result.First().Date.Day, Is.EqualTo(08));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = temperaturesService.GetAverageTemperaturesForTheYear(new DateOnly(2025, random.Next(1,12), random.Next(1,30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2025));
            Assert.That(result.First().Date.Month, Is.EqualTo(01));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}