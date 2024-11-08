using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
using ScientificOperationsCenter.Api.ViewModels;
using Moq;
using ScientificOperationsCenter.Api.DAL.Interfaces;
========
using ScientificOperationsCenter.ViewModels;
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs


namespace ScientificOperationsCenter.Tests
{
    class RadiationMeasurementsServiceMapperIntegrationTest
    {

        private Mock<IRadiationMeasurementsRepository> _radiationMeasurementsRepositoryMock;
        private RadiationMeasurementsService _radiationMeasurementsService;
        private RadiationMeasurementsMapper _radiationMeasurementsMapper;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            _radiationMeasurementsService = new RadiationMeasurementsService(_radiationMeasurementsRepositoryMock.Object);
            _radiationMeasurementsMapper = new RadiationMeasurementsMapper(_radiationMeasurementsService);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsTimeViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo((new TimeOnly(06, 00)).ToString()));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(280));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Hour, Is.EqualTo(new TimeOnly(6, 00)));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(280));
            Assert.That(mapperResult.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(230));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo("8"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo("9"));
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("8"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(410));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("9"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(510));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsDateViewModelReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().TimeFrame, Is.EqualTo("October"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(920));
            Assert.That(mapperResult.Last().TimeFrame, Is.EqualTo("November"));
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(date);

            // Assert
            Assert.NotNull(mapperResult);
            Assert.That(mapperResult.First().Date, Is.EqualTo("October"));
            Assert.That(mapperResult.First().TotalRadiation, Is.EqualTo(920));
            Assert.That(mapperResult.Last().Date, Is.EqualTo("November"));
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            Assert.That(mapperResult.Last().TotalRadiation, Is.EqualTo(300));
            Assert.That(mapperResult.Count(), Is.EqualTo(2));
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 10, 01);

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsTimeViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsTimeViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2024, 09, _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }


        [Test]
        public void GivenARepositoryOfRadiationMeasurements_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2026, _random.Next(1, 12), _random.Next(1, 30));

            // Action
<<<<<<<< HEAD:ScientificOperationsCenter.Api.Tests/IntegrationTests/RadiationMeasurementsServiceMapperIntegrationTest.cs
            var mapperResult = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);
========
            var mapperResult = radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(date);
>>>>>>>> stable:ScientificOperationsCenter.Api.Tests/RadiationMeasurementsServiceMapperIntegrationTest.cs

            // Assert
            Assert.That(mapperResult.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<RadiationMeasurementsDateViewModel>>(mapperResult, "The returned element is not of IEnumerable<RadiationMeasurementsDateViewModel> type.");
        }
    }
}
