using Moq;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Tests.Mocks;


namespace ScientificOperationsCenter.Api.Tests.UnitTests
{
    public class RadiationMeasurementsServiceUnitTest
    {
        private Mock<IRadiationMeasurementsRepository> _radiationMeasurementsRepositoryMock;
        private RadiationMeasurementsService _radiationMeasurementsService;
        private Random _random;


        [SetUp]
        public void SetUp()
        {
            _radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            _radiationMeasurementsService = new RadiationMeasurementsService(_radiationMeasurementsRepositoryMock.Object);
            _random = new Random();
        }


        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfSameDaySumHourRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, 09);

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Time, Is.EqualTo(new TimeOnly(21, 00)));
                Assert.That(result.First().TotalMilligrays, Is.EqualTo(230));
                Assert.That(result.Last().Time, Is.EqualTo(new TimeOnly(06, 00)));
                Assert.That(result.Last().TotalMilligrays, Is.EqualTo(280));
                Assert.That(result.Count(), Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfSameMonthSumEachDayOfTheMonthRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2024, 10, 08)));
                Assert.That(result.First().TotalMilligrays, Is.EqualTo(410));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2024, 10, 09)));
                Assert.That(result.Last().TotalMilligrays, Is.EqualTo(510));
                Assert.That(result.Count(), Is.EqualTo(2));
            });
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfSameYearSumEachMonthOfTheYearRadiationMeasurementsReturn()
        {
            // Setup
            var date = new DateOnly(2025, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.First().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.First().TotalMilligrays, Is.EqualTo(400));
                Assert.That(result.Last().Date, Is.EqualTo(new DateOnly(2025, 01, 01)));
                Assert.That(result.Last().TotalMilligrays, Is.EqualTo(400));
                Assert.That(result.Count(), Is.EqualTo(1));
            });
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByDay_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 10, 30);

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<RadiationMeasurementsTimeSum>>(),
                    "The returned element is not of IEnumerable<RadiationMeasurementsTimeSum> type.");
            });
        }


        [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByMonth_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2025, 09, _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<RadiationMeasurementsDateSum>>(),
                    "The returned element is not of IEnumerable<RadiationMeasurementsDateSum> type.");
            });
        }


            [Test]
        public async Task GivenARepositoryOfRadiationMeasurements_WhenGettingRadiationMeasurementsByYear_ThenIfEmptyEmptyIEnumerableReturn()
        {
            // Setup
            var date = new DateOnly(2026, _random.Next(1, 12), _random.Next(1, 30));

            // Action
            var result = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(date);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(result, Is.InstanceOf<IEnumerable<RadiationMeasurementsDateSum>>(),
                    "The returned element is not of IEnumerable<RadiationMeasurementsDateSum> type.");
            });
        }


        [Test]
        public void Constructor_WhenRadiationMeasurementsRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IRadiationMeasurementsRepository? radiationMeasurementsRepository = null;

            // Action & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _ = new RadiationMeasurementsService(radiationMeasurementsRepository!));
            Assert.That(exception.ParamName, Is.EqualTo("radiationMeasurementsRepository"));
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingRadiationMeasurementsByDay_ThenIfBusinessLogicExceptionReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            radiationMeasurementsRepositoryMock.Setup(m => m.GetByDayAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from service"));
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var date = new DateOnly(2024, 10, 08);

            try
            {
                // Action
                var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from service"));
            }
        }


        [Test]
        public async Task GivenARadiationMeasurementsService_WhenGettingRadiationMeasurementsByMonth_ThenIfDataAccessExceptionReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            radiationMeasurementsRepositoryMock.Setup(m => m.GetByMonthAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from service"));
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var date = new DateOnly(2024, 10, _random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from service"));
            }
        }


        [Test]
        public async Task GivenARadiationMeasurementsRepository_WhenGettingRadiationMeasurementsByYear_ThenIfDataAccessExceptionReturn()
        {
            // Setup
            var radiationMeasurementsRepositoryMock = MockIRadiationMeasurementsRepository.GetMock();
            radiationMeasurementsRepositoryMock.Setup(m => m.GetByYearAsync(It.IsAny<DateOnly>()))
                .Throws(new DataAccessException("Verfiy DataAccessException is passed from service"));
            var radiationMeasurementsService = new RadiationMeasurementsService(radiationMeasurementsRepositoryMock.Object);
            var date = new DateOnly(2024, _random.Next(1, 12), _random.Next(1, 30));

            try
            {
                // Action
                var result = await radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(date);
                Assert.Fail();
            }
            catch (Exception gEx)
            {
                // Assert
                Assert.That(gEx, Is.Not.Null);
                Assert.That(gEx, Is.InstanceOf<DataAccessException>());
                var dataAccessExceptionResult = gEx as DataAccessException;
                Assert.That(dataAccessExceptionResult?.Message,
                    Is.EqualTo("Verfiy DataAccessException is passed from service"));
            }
        }
    }
}