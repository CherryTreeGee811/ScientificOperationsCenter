using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    internal class RadiationMeasurementsMapperUnitTest
    {
        private Mock<IRadiationMeasurementsService> _radiationMeasurementsServiceMock;
        private RadiationMeasurementsMapper _radiationMeasurementsMapper;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsServiceMock = MockIRadiationMeasurementsService.GetMock();
            _radiationMeasurementsMapper = new RadiationMeasurementsMapper(_radiationMeasurementsServiceMock.Object);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByHourOfDay_ThenCollectionOfRadiationMeasurementsTimeViewModelSortedByHourReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
                Assert.That(result.First().TotalRadiation, Is.EqualTo(430));
                Assert.That(result.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
                Assert.That(result.Count(), Is.EqualTo(6));
            });
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByDayOfMonth_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByDayReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo("1"));
                Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
                Assert.That(result.Last().TimeFrame, Is.EqualTo("21"));
                Assert.That(result.Last().TotalRadiation, Is.EqualTo(110));
                Assert.That(result.Count(), Is.EqualTo(7));
            });
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingSummedRadiationMeasurementsByMonthOfYear_ThenCollectionOfRadiationMeasurementsDateViewModelSortedByMonthReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo("May"));
                Assert.That(result.First().TotalRadiation, Is.EqualTo(120));
                Assert.That(result.Last().TimeFrame, Is.EqualTo("December"));
                Assert.That(result.Last().TotalRadiation, Is.EqualTo(150));
                Assert.That(result.Count(), Is.EqualTo(8));
            });
        }


        [Test]
        public void Constructor_WhenRadiationMeasurementsServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IRadiationMeasurementsService? radiationMeasurementsService = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _ = new RadiationMeasurementsMapper(radiationMeasurementsService!));
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
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<BusinessLogicException>());
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult?.Message,
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
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<BusinessLogicException>());
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult?.Message,
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
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<BusinessLogicException>());
                var businessLogicExceptionResult = gEx as BusinessLogicException;
                Assert.That(businessLogicExceptionResult?.Message,
                    Is.EqualTo("Verfiy BusinessLogicException is passed from mapper"));
            }
        }
    }
}
