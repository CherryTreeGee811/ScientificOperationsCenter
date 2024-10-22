using Moq;
using ScientificOperationsCenter.DAL.Interfaces;
using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.Tests.Mocks
{
    internal sealed class MockIRadiationMeasurementsRepository
    {
        public static Mock<IRadiationMeasurementsRepository> GetMock()
        {
            var mock = new Mock<IRadiationMeasurementsRepository>();


            var radiationMeasurements = new List<RadiationMeasurements>()
            {
                new RadiationMeasurements { Id = 1, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), Milligrays = 100 },
                new RadiationMeasurements { Id = 2, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), Milligrays = 120 },
                new RadiationMeasurements { Id = 3, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), Milligrays = 120 },
                new RadiationMeasurements { Id = 4, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), Milligrays = 110 },
                new RadiationMeasurements { Id = 5, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(04, 30), Milligrays = 200 },
                new RadiationMeasurements { Id = 6, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 00), Milligrays = 160 },
                new RadiationMeasurements { Id = 7, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), Milligrays = 190 },
                new RadiationMeasurements { Id = 8, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), Milligrays = 100 },
                new RadiationMeasurements { Id = 9, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(04, 30), Milligrays = 200 },
                new RadiationMeasurements { Id = 10, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(04, 30), Milligrays = 200 },
                new RadiationMeasurements { Id = 11, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 40), Milligrays = 120 }
            };

            mock.Setup(m => m.GetByDayAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) => 
                radiationMeasurements.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day));

            mock.Setup(m => m.GetByMonthAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) => 
                radiationMeasurements.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month));

            mock.Setup(m => m.GetByYearAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) => 
                radiationMeasurements.Where(x => x.Date.Year == date.Year));

            return mock;
        }
    }
}