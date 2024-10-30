using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests.UnitTests
{
    internal class RadiationMeasurementsMapperUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenCollectionOfRadiationMeasurementsTimeViewModelSortedByHourReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);

            // Action
            var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Timeframe, Is.EqualTo("1:00 AM"));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(result.Last().Timeframe, Is.EqualTo("9:00 PM"));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(result.Count(), Is.EqualTo(6));
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByDayReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Timeframe, Is.EqualTo("1"));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(result.Last().Timeframe, Is.EqualTo("21"));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(result.Count(), Is.EqualTo(7));
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByMonthReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();

            // Action
            var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(new DateOnly(2024, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Timeframe, Is.EqualTo("May"));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(result.Last().Timeframe, Is.EqualTo("December"));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(150));
            Assert.That(result.Count(), Is.EqualTo(8));
        }
    }
}
