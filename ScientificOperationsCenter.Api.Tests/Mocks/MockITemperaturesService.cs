using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal sealed class MockITemperaturesService
    {
        public static Mock<ITemperaturesService> GetMock()
        {
            var mock = new Mock<ITemperaturesService>();


            var temperaturesSameDay = new List<Temperatures>()
            {
                new() { Id = 1, Date = new (2024, 10, 09), Time = new (01, 00), TemperatureCelcius = -4 },
                new() { Id = 2, Date = new (2024, 10, 09), Time = new (01, 20), TemperatureCelcius = -9 },
                new() { Id = 3, Date = new (2024, 10, 09), Time = new (01, 50), TemperatureCelcius = 10 },
                new() { Id = 4, Date = new (2024, 10, 09), Time = new (6, 30), TemperatureCelcius = 20 },
                new() { Id = 5, Date = new (2024, 10, 09), Time = new (21, 02), TemperatureCelcius = 30 },
                new() { Id = 6, Date = new (2024, 10, 09), Time = new (06, 50), TemperatureCelcius = 12 },
                new() { Id = 7, Date = new (2024, 10, 09), Time = new (03, 00), TemperatureCelcius = -2 },
                new() { Id = 8, Date = new (2024, 10, 09), Time = new (06, 20), TemperatureCelcius = 21 },
                new() { Id = 9, Date = new (2024, 10, 09), Time = new (04, 00), TemperatureCelcius = 23 },
                new() { Id = 10, Date = new (2024, 10, 09), Time = new (02, 10), TemperatureCelcius = 0 },
            };


            var temperaturesSameMonth = new List<Temperatures>()
            {
                new() { Id = 1, Date = new (2024, 10, 09), Time = new (6, 30), TemperatureCelcius = -1 },
                new() { Id = 2, Date = new (2024, 10, 02), Time = new (21, 00), TemperatureCelcius = 33 },
                new() { Id = 3, Date = new (2024, 10, 11), Time = new (11, 07), TemperatureCelcius = 12 },
                new() { Id = 4, Date = new (2024, 10, 20), Time = new (08, 02), TemperatureCelcius = 1 },
                new() { Id = 5, Date = new (2024, 10, 21), Time = new (02, 03), TemperatureCelcius = 20 },
                new() { Id = 6, Date = new (2024, 10, 21), Time = new (02, 30), TemperatureCelcius = 10 },
                new() { Id = 7, Date = new (2024, 10, 06), Time = new (05, 30), TemperatureCelcius = 4 },
                new() { Id = 8, Date = new (2024, 10, 02), Time = new (07, 02), TemperatureCelcius = 5 },
                new() { Id = 9, Date = new (2024, 10, 01), Time = new (08, 30), TemperatureCelcius = 32 },
                new() { Id = 10, Date = new (2024, 10, 01), Time = new (08, 30), TemperatureCelcius = 22 },
                new() { Id = 11, Date = new (2024, 10, 01), Time = new (08, 30), TemperatureCelcius = 28 }
            };


            var temperaturesSameYear = new List<Temperatures>()
            {
                new() { Id = 1, Date = new (2025, 05, 21), Time = new (6, 30), TemperatureCelcius = -2 },
                new() { Id = 2, Date = new (2025, 05, 20), Time = new (6, 30), TemperatureCelcius = -4 },
                new() { Id = 3, Date = new (2025, 06, 09), Time = new (11, 07), TemperatureCelcius = 0 },
                new() { Id = 4, Date = new (2025, 07, 10), Time = new (12, 00), TemperatureCelcius = 6 },
                new() { Id = 5, Date = new (2025, 08, 12), Time = new (13, 10), TemperatureCelcius = 15 },
                new() { Id = 6, Date = new (2025, 09, 02), Time = new (14, 20), TemperatureCelcius = 19 },
                new() { Id = 7, Date = new (2025, 10, 01), Time = new (09, 30), TemperatureCelcius = 20 },
                new() { Id = 8, Date = new (2025, 11, 26), Time = new (03, 00), TemperatureCelcius = 12 },
                new() { Id = 9, Date = new (2025, 12, 29), Time = new (02, 00), TemperatureCelcius = 20 },
                new() { Id = 10, Date = new (2025, 12, 30), Time = new (02, 00), TemperatureCelcius = 34 }
            };


            mock.Setup(m => m.GetAverageTemperaturesForTheDayAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameDay.GroupBy(t => t.Time.Hour)
                    .Select(t => new TemperaturesTimeAverage { Time = new TimeOnly(t.Key, 00), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) }));

            mock.Setup(m => m.GetAverageTemperaturesForTheMonthAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameMonth.GroupBy(t => t.Date.Day)
                    .Select(t => new TemperaturesDateAverage { Date = new DateOnly(date.Year, date.Month, t.Key), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) }));

            mock.Setup(m => m.GetAverageTemperaturesForTheYearAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameYear.GroupBy(t => t.Date.Month)
                    .Select(t => new TemperaturesDateAverage { Date = new DateOnly(date.Year, t.Key, 01), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) }));


            return mock;
        }
    }
}