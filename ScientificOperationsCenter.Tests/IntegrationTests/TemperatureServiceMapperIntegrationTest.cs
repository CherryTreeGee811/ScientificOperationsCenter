using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Tests.IntegrationTests
{
    internal class TemperatureServiceMapperIntegrationTest
    {
        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesTimeViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var date = new DateOnly(2024, 10, 08);

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheDayAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo("12:00 PM"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(9));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo("7:00 PM"));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(17));
            Assert.That(mapperResult.Count(), Is.EqualTo(3));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesDateViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo("8"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo("9"));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesDateViewModelReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheYearAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Timeframe, Is.EqualTo("October"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Timeframe, Is.EqualTo("November"));
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(2));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2025, 10, 30);

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheDayAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2025, 09, random.Next(1, 30));

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesViewModel> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var temperaturesMapper = new TemperaturesMapper(temperaturesService);
            var random = new Random();
            var date = new DateOnly(2026, random.Next(1, 12), random.Next(1, 30));

            // Action
            var mapperResult = await temperaturesMapper.GetTemperaturesForTheYearAsync(date);

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesViewModel> type.");
        }
    }
}
