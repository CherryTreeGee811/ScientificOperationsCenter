using Moq;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal sealed class MockITemperaturesRepository
    {
        public static Mock<ITemperaturesRepository> GetMock()
        {
            var mock = new Mock<ITemperaturesRepository>();


            var temperatures = new List<Temperatures>()
            {
                new() { Id = 1, Date = new (2024, 10, 08), Time = new (16, 00), TemperatureCelcius = -4 },
                new() { Id = 2, Date = new (2024, 10, 08), Time = new (19, 00), TemperatureCelcius = 12 },
                new() { Id = 3, Date = new (2024, 10, 08), Time = new (19, 20), TemperatureCelcius = 22 },
                new() { Id = 4, Date = new (2024, 10, 08), Time = new (12, 00), TemperatureCelcius = 8 },
                new() { Id = 5, Date = new (2024, 11, 03), Time = new (04, 30), TemperatureCelcius = 5 },
                new() { Id = 6, Date = new (2024, 10, 09), Time = new (21, 30), TemperatureCelcius = 12 },
                new() { Id = 7, Date = new (2024, 10, 08), Time = new (12, 40), TemperatureCelcius = 10 },
                new() { Id = 8, Date = new (2025, 01, 03), Time = new (05, 30), TemperatureCelcius = 15 },
                new() { Id = 9, Date = new (2024, 10, 09), Time = new (21, 00), TemperatureCelcius = 10 },
                new() { Id = 10, Date = new (2024, 10, 09), Time = new (21, 31), TemperatureCelcius = 9 },
                new() { Id = 11, Date = new (2024, 11, 02), Time = new (09, 10), TemperatureCelcius = -2 },
                new() { Id = 12, Date = new (2025, 01, 05), Time = new (03, 30), TemperatureCelcius = 5 }
            };


            mock.Setup(m => m.GetByDayAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperatures.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day) );

            mock.Setup(m => m.GetByMonthAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperatures.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month));

            mock.Setup(m => m.GetByYearAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) => 
                temperatures.Where(x => x.Date.Year == date.Year));


            return mock;
        }
    }
}
