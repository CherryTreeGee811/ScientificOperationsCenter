using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.CustomExceptions;
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
            Assert.That(result.First().Timeframe, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
            Assert.That(result.First().TotalRadiation, Is.EqualTo(430));
            Assert.That(result.Last().Timeframe, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
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


        [Test]
        public void Constructor_WhenRadiationMeasurementsServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IRadiationMeasurementsService radiationMeasurementsService = null;

            // Action
            TestDelegate action = () => new RadiationMeasurementsMapper(radiationMeasurementsService);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.That(exception.ParamName, Is.EqualTo("radiationMeasurementsService"));
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingRadiationMeasurementsByDay_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            radiationMeasurementsServiceMock.Setup(m => m.GetRadiationMeasurementsSumForTheDayAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var date = new DateOnly(2024, 10, 08);

            try
            {
                // Action
                var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<BusinessLogicException>(gEx);
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult.Message,
                    Is.EqualTo("Verfiy BusinessLogicException is passed from mapper"));
            }
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingRadiationMeasurementsByMonth_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            radiationMeasurementsServiceMock.Setup(m => m.GetRadiationMeasurementsSumForTheMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<BusinessLogicException>(gEx);
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult.Message,
                    Is.EqualTo("Verfiy BusinessLogicException is passed from mapper"));
            }
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingRadiationMeasurementsByYear_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            radiationMeasurementsServiceMock.Setup(m => m.GetRadiationMeasurementsSumForTheYearAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var radiationMeasurementsMapper = new RadiationMeasurementsMapper(radiationMeasurementsServiceMock.Object);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<BusinessLogicException>(gEx);
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult.Message,
                    Is.EqualTo("Verfiy BusinessLogicException is passed from mapper"));
            }
        }
    }
}
