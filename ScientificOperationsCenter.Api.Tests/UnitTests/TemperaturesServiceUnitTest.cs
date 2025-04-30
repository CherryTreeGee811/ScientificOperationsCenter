using Moq;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    public class TemperaturesServiceUnitTest
    {
        private Mock<ITemperaturesRepository> _temperaturesRepositoryMock;
        private TemperaturesService _temperaturesService;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            _temperaturesService = new TemperaturesService(_temperaturesRepositoryMock.Object);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(21, 00)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Count(), Is.EqualTo(1));
            });
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 10, 08)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Count(), Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesReturn()
        {
            // Setup
            var date = new DateOnly(2025, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.Last().AverageTemperature, Is.EqualTo(10));
                Assert.That(result.Count(), Is.EqualTo(1));
            });
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(1900, 01, 01);

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<TemperaturesTimeAverage>>(),
                    "The returned element is not of IEnumerable<TemperaturesTimeAverage> type.");
            });
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(1900, 01, 01);

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<TemperaturesDateAverage>>(),
                    "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
            });
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(1900, 01, 01);

            // Action
            var result = await _temperaturesService.GetAverageTemperaturesForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<TemperaturesDateAverage>>(),
                    "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
            });
        }


        [Test]
        public void Constructor_WhenTemperaturesRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITemperaturesRepository? temperaturesRepository = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _ = new TemperaturesService(temperaturesRepository!));
            Assert.That(exception.ParamName, Is.EqualTo("temperaturesRepository"));
        }


        [Test]
        public async Task GivenATemperaturesRepository_GetAverageTemperaturesForTheDayAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            temperaturesRepositoryMock.Setup(m => m.GetByDayAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from mapper"));
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var date = new DateOnly(2024, 10, 08);

            try
            {
                // Action
                var result = await temperaturesService.GetAverageTemperaturesForTheDayAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from mapper"));
            }
        }


        [Test]
        public async Task GivenATemperaturesRepository_GetAverageTemperaturesForTheMonthAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            temperaturesRepositoryMock.Setup(m => m.GetByMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from mapper"));
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from mapper"));
            }
        }


        [Test]
        public async Task GivenATemperaturesRepository_GetAverageTemperaturesForTheYearAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            temperaturesRepositoryMock.Setup(m => m.GetByYearAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from mapper"));
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from mapper"));
            }
        }
    }
}