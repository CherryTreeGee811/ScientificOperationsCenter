using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal sealed class MockITemperaturesMapper
    {
        public static Mock<ITemperaturesMapper> GetMock()
        {
            var mock = new Mock<ITemperaturesMapper>();


            var temperaturesSameDay = new List<TemperaturesTimeAverage>()
            {
                new() { Time = new (01, 00), AverageTemperature = -4 },
                new() { Time = new (21, 00), AverageTemperature = 30 },
                new() { Time = new (06, 00), AverageTemperature = 12 },
                new() { Time = new (03, 00), AverageTemperature = -2 },
                new() { Time = new (04, 00), AverageTemperature = 23 },
                new() { Time = new (02, 00), AverageTemperature = 0 },
            };


            var temperaturesSameMonth = new List<TemperaturesDateAverage>()
            {
                new() { Date = new (2024, 10, 09), AverageTemperature = -1 },
                new() { Date = new (2024, 10, 02), AverageTemperature = 33 },
                new() { Date = new (2024, 10, 11), AverageTemperature = 12 },
                new() { Date = new (2024, 10, 20), AverageTemperature = 1 },
                new() { Date = new (2024, 10, 21), AverageTemperature = 20 },
                new() { Date = new (2024, 10, 06), AverageTemperature = 4 },
                new() { Date = new (2024, 10, 01), AverageTemperature = 32 },
            };


            var temperaturesSameYear = new List<TemperaturesDateAverage>()
            {
                new() { Date = new (2025, 10, 01), AverageTemperature = 20 },
                new() { Date = new (2025, 06, 01), AverageTemperature = 0 },
                new() { Date = new (2025, 07, 01), AverageTemperature = 6 },
                new() { Date = new (2025, 12, 01), AverageTemperature = 20 },
                new() { Date = new (2025, 08, 01), AverageTemperature = 15 },
                new() { Date = new (2025, 09, 01), AverageTemperature = 19 },
                new() { Date = new (2025, 11, 01), AverageTemperature = 12 },
                new() { Date = new (2025, 05, 01), AverageTemperature = -2 },
            };


            mock.Setup(m => m.GetTemperaturesForTheDayAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameDay.OrderBy(t => t.Time.Hour).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Time.ToString(), AverageTemperature = t.AverageTemperature }));

            mock.Setup(m => m.GetTemperaturesForTheMonthAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameMonth.OrderBy(t => t.Date.Day).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Date.Day.ToString(), AverageTemperature = t.AverageTemperature }));

            mock.Setup(m => m.GetTemperaturesForTheYearAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                temperaturesSameYear.OrderBy(t => t.Date.Month).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Date.ToString("MMMM"), AverageTemperature = t.AverageTemperature }));


            return mock;
        }
    }
}