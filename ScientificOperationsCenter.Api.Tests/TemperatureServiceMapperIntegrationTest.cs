using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
using ScientificOperationsCenter.Api.ViewModels;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using Moq;
========
using ScientificOperationsCenter.ViewModels;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs


namespace ScientificOperationsCenter.Tests
{
    internal class TemperatureServiceMapperIntegrationTest
    {
        private Mock<ITemperaturesRepository> _temperaturesRepository;
        private TemperaturesService _temperaturesService;
        private TemperaturesMapper _temperaturesMapper;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _temperaturesRepository = MockITemperaturesRepository.GetMock();
            _temperaturesService = new TemperaturesService(_temperaturesRepository.Object);
            _temperaturesMapper = new TemperaturesMapper(_temperaturesService);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesTimeViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 08);

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheDayAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo((new TimeOnly(12, 00)).ToString()));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(9));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo((new TimeOnly(19, 00)).ToString()));
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheDay(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Hour, Is.EqualTo(new TimeOnly(12, 00)));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(9));
            Assert.That(mapperResult.Last().Hour, Is.EqualTo(new TimeOnly(19, 00)));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(17));
            Assert.That(mapperResult.Count(), Is.EqualTo(3));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo("8"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo("9"));
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheMonth(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("8"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("9"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheYearAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo("October"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo("November"));
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheYear(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("October"));
            Assert.That(mapperResult.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("November"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().AverageTemperature, Is.EqualTo(2));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 10, 30);

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheDayAsync(date);
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheDay(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesTimeViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 09, _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheMonthAsync(date);
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheMonth(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesDateViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2026, _random.Next(1, 12), _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/TemperatureServiceMapperIntegrationTest.cs
            var mapperResult = await _temperaturesMapper.GetTemperaturesForTheYearAsync(date);
========
            var mapperResult = temperaturesMapper.GetTemperaturesForTheYear(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/TemperatureServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<TemperaturesDateViewModel> type.");
        }
    }
}
