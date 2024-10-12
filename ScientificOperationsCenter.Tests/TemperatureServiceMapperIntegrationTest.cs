using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Tests
{
    internal class TemperatureServiceMapperIntegrationTest
    {
        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesTimeViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var date = new DateOnly(2024, 10, 08);

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheDay(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheDay(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Hour, Is.EqualTo(new TimeOnly(12, 00)));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(9));
            Assert.That(mapperResult.Last().Hour, Is.EqualTo(new TimeOnly(19, 00)));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(17));
            Assert.That(mapperResult.Count(), Is.EqualTo(3));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesDateViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheMonth(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheMonth(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("8"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("9"));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesDateViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheYear(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheYear(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("October"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("November"));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(2));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2025, 10, 30);

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheDay(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheDay(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesTimeViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2025, 09, random.Next(1, 30));

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheMonth(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheMonth(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesDateViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2026, random.Next(1, 12), random.Next(1, 30));

            // Action
            var serviceResult = temperaturesService.GetAverageTemperaturesForTheYear(date);
            var mapperResult = temperaturesMapper.GetTemperaturesForTheYear(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesDateViewModel> type.");
        }
    }
}
