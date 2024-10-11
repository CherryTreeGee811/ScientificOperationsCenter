using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests
{
    internal class TemperaturesMapperUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByHourOfDay_ThenCollectionOfTemperaturesTimeViewModelSortedByHourReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);

            // Action
            var result = temperaturesMapper.GetTemperaturesForTheDay(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Hour, Is.EqualTo(new TimeOnly(1, 00)));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(-1));
            Assert.That(result.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.Last().AverageTemperature, Is.EqualTo(30));
            Assert.That(result.Count(), Is.EqualTo(6));
        }


        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByDayOfMonth_ThenCollectionOfTemperaturesDateViewModelSortedByDayReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var random = new Random();

            // Action
            var result = temperaturesMapper.GetTemperaturesForTheMonth(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date, Is.EqualTo("1"));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(27));
            Assert.That(result.Last().Date, Is.EqualTo("21"));
            Assert.That(result.Last().AverageTemperature, Is.EqualTo(15));
            Assert.That(result.Count(), Is.EqualTo(7));
        }


        [Test]
        public void GivenATemperaturesService_WhenGettingAverageTemperaturesByMonthOfYear_ThenCollectionOfTemperaturesDateViewModelSortedByMonthReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var random = new Random();

            // Action
            var result = temperaturesMapper.GetTemperaturesForTheYear(new DateOnly(2024, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date, Is.EqualTo("May"));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(-3));
            Assert.That(result.Last().Date, Is.EqualTo("December"));
            Assert.That(result.Last().AverageTemperature, Is.EqualTo(27));
            Assert.That(result.Count(), Is.EqualTo(8));
        }
    }
}
