using Moq;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal sealed class MockIRadiationMeasurementsRepository
    {
        public static Mock<IRadiationMeasurementsRepository> GetMock()
        {
            var mock = new Mock<IRadiationMeasurementsRepository>();


            var radiationMeasurements = new List<RadiationMeasurements>()
            {
                new() { Id = 1, Date = new (2024, 10, 08), Time = new (16, 00), Milligrays = 100 },
                new() { Id = 2, Date = new (2024, 10, 08), Time = new (19, 00), Milligrays = 120 },
                new() { Id = 3, Date = new (2024, 10, 09), Time = new (21, 30), Milligrays = 120 },
                new() { Id = 4, Date = new (2024, 10, 09), Time = new (21, 00), Milligrays = 110 },
                new() { Id = 5, Date = new (2025, 01, 05), Time = new (04, 30), Milligrays = 200 },
                new() { Id = 6, Date = new (2024, 10, 09), Time = new (06, 00), Milligrays = 160 },
                new() { Id = 7, Date = new (2024, 10, 08), Time = new (12, 00), Milligrays = 190 },
                new() { Id = 8, Date = new (2024, 11, 02), Time = new (09, 10), Milligrays = 100 },
                new() { Id = 9, Date = new (2024, 11, 03), Time = new (04, 30), Milligrays = 200 },
                new() { Id = 10, Date = new (2025, 01, 03), Time = new (04, 30), Milligrays = 200 },
                new() { Id = 11, Date = new (2024, 10, 09), Time = new (06, 40), Milligrays = 120 }
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