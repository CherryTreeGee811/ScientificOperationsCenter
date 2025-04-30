using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    internal class TemperaturesMapperUnitTest
    {
        private Mock<ITemperaturesService> _temperaturesServiceMock;
        private TemperaturesMapper _temperaturesMapper;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _temperaturesServiceMock = MockITemperaturesService.GetMock();
            _temperaturesMapper = new TemperaturesMapper(_temperaturesServiceMock.Object);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByHourOfDay_ThenCollectionOfTemperaturesTimeViewModelSortedByHourReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _temperaturesMapper.GetTemperaturesForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo((new TimeOnly(1, 00)).ToString()));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(-1));
                Assert.That(result.Last().TimeFrame, Is.EqualTo((new TimeOnly(21, 00)).ToString()));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(30));
                Assert.That(result.Count(), Is.EqualTo(6));
            });
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByDayOfMonth_ThenCollectionOfTemperaturesDateViewModelSortedByDayReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _temperaturesMapper.GetTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo("1"));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(27));
                Assert.That(result.Last().TimeFrame, Is.EqualTo("21"));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(15));
                Assert.That(result.Count(), Is.EqualTo(7));
            });
        }


        [Test]
        public async Task GivenATemperaturesService_WhenGettingAverageTemperaturesByMonthOfYear_ThenCollectionOfTemperaturesDateViewModelSortedByMonthReturn()
        {
            // Setup
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _temperaturesMapper.GetTemperaturesForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().TimeFrame, Is.EqualTo("May"));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(-3));
                Assert.That(result.Last().TimeFrame, Is.EqualTo("December"));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(27));
                Assert.That(result.Count(), Is.EqualTo(8));
            });
        }


        [Test]
        public void Constructor_WhenTemperaturesServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITemperaturesService? temperaturesService = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _ = new TemperaturesMapper(temperaturesService!));
            Assert.That(exception.ParamName, Is.EqualTo("temperaturesService"));
        }


        [Test]
        public async Task GivenATemperaturesService_GetAverageTemperaturesForTheDayAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            temperaturesServiceMock.Setup(m => m.GetAverageTemperaturesForTheDayAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var date = new DateOnly(2024, 10, 08);

            try
            {
                // Action
                var result = await temperaturesMapper.GetTemperaturesForTheDayAsync(date);
                Assert.Fail();
            } 
            catch(Exception gEx)
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
        public async Task GivenATemperaturesService_GetAverageTemperaturesForTheMonthAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            temperaturesServiceMock.Setup(m => m.GetAverageTemperaturesForTheMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesMapper.GetTemperaturesForTheMonthAsync(date);
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
        public async Task GivenATemperaturesService_GetAverageTemperaturesForTheYearAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesServiceMock = MockITemperaturesService.GetMock();
            temperaturesServiceMock.Setup(m => m.GetAverageTemperaturesForTheYearAsync(It.IsAny<DateOnly>()))
                .Throws(new BusinessLogicException("Verfiy BusinessLogicException is passed from mapper"));
            var temperaturesMapper = new TemperaturesMapper(temperaturesServiceMock.Object);
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesMapper.GetTemperaturesForTheMonthAsync(date);
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
