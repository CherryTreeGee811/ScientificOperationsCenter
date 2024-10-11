using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;
using System;


namespace ScientificOperationsCenter.Tests
{
    internal class RadiationMeasurementsMapperUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenCollectionOfRadiationMeasurementsTimeViewModelSortedByHourReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);

            // Action
            var result = radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Hour, Is.EqualTo(new TimeOnly(1, 00)));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(result.Last().Hour, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(result.Count(), Is.EqualTo(6));
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByDayReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();

            // Action
            var result = radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date, Is.EqualTo("1"));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(result.Last().Date, Is.EqualTo("21"));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
            Assert.That(result.Count(), Is.EqualTo(7));
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByMonthReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();

            // Action
            var result = radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(new DateOnly(2024, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date, Is.EqualTo("May"));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
            Assert.That(result.Last().Date, Is.EqualTo("December"));
            Assert.That(result.Last().TotalRadiation, Is.EqualTo(150));
            Assert.That(result.Count(), Is.EqualTo(8));
        }
    }
}
