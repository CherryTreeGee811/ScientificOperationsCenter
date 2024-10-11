using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests
{
    internal class RadiationMeasurementsMapperUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenIfReturnRadiationMeasurementsViewModelsContainingDataReturn()
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
            Assert.That(result.Count(), Is.EqualTo(6));
        }
    }
}
