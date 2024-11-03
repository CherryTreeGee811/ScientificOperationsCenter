using Moq;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Mappers;
using ScientificOperationsCenter.Tests.Mocks;


namespace ScientificOperationsCenter.Tests.UnitTests
{
    public class TemperaturesServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfSameDayAverageHourTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheDayAsync(new DateOnly(2024, 10, 09));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfSameMonthAverageDayOfMonthTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            // 08
            var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(new DateOnly(2024, 10, random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2024));
            Assert.That(result.First().Date.Month, Is.EqualTo(10));
            Assert.That(result.First().Date.Day, Is.EqualTo(08));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfSameYearAverageMonthOfYearTemperaturesReturn()
        {
            // Setup
            var temperatureRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperatureRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(new DateOnly(2025, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.NotNull(result);
            Assert.That(result.First().Date.Year, Is.EqualTo(2025));
            Assert.That(result.First().Date.Month, Is.EqualTo(01));
            Assert.That(result.First().AverageTemperature, Is.EqualTo(10));
            Assert.That(result.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheDayAsync(new DateOnly(2025, 10, 30));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesTimeAverage>>(result, "The returned element is not of IEnumerable<TemperaturesTimeAverage> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(new DateOnly(2025, 09, random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateAverage>>(result, "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
        }


        [Test]
        public async Task GivenARepositoryOfTemperatures_WhenGettingTemperaturesByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();

            // Action
            var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(new DateOnly(2026, random.Next(1, 12), random.Next(1, 30)));

            // Assert
            Assert.That(result.Any(), Is.EqualTo(false));
            Assert.IsInstanceOf<IEnumerable<TemperaturesDateAverage>>(result, "The returned element is not of IEnumerable<TemperaturesDateAverage> type.");
        }


        [Test]
        public void Constructor_WhenTemperaturesRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITemperaturesRepository temperaturesRepository = null;

            // Action
            TestDelegate action = () => new TemperaturesService(temperaturesRepository);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
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
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<DataAccessException>(gEx);
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult.Message,
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
            var random = new Random();
            var date = new DateOnly(2024, 10, random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<DataAccessException>(gEx);
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from mapper"));
            }
        }


        public async Task GivenATemperaturesRepository_GetAverageTemperaturesForTheYearAsync_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var temperaturesRepositoryMock = MockITemperaturesRepository.GetMock();
            temperaturesRepositoryMock.Setup(m => m.GetByYearAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from mapper"));
            var temperaturesService = new TemperaturesService(temperaturesRepositoryMock.Object);
            var random = new Random();
            var date = new DateOnly(2024, random.Next(1, 12), random.Next(1, 30));

            try
            {
                // Action
                var result = await temperaturesService.GetAverageTemperaturesForTheYearAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.NotNull(gEx);
                Assert.IsInstanceOf<DataAccessException>(gEx);
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from mapper"));
            }
        }
    }
}