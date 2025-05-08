using Moq;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal sealed class MockIRadiationMeasurementsService
    {
        public static Mock<IRadiationMeasurementsService> GetMock()
        {
            var mock = new Mock<IRadiationMeasurementsService>();


            var radiationMeasurementsSameDay = new List<RadiationMeasurements>()
            {
                new() { Id = 1, Date = new (2024, 10, 09), Time = new (01, 00), Milligrays = 160 },
                new() { Id = 2, Date = new (2024, 10, 09), Time = new (01, 20), Milligrays = 150 },
                new() { Id = 3, Date = new (2024, 10, 09), Time = new (01, 50), Milligrays = 120 },
                new() { Id = 4, Date = new (2024, 10, 09), Time = new (6, 30), Milligrays = 120 },
                new() { Id = 5, Date = new (2024, 10, 09), Time = new (21, 02), Milligrays = 110 },
                new() { Id = 6, Date = new (2024, 10, 09), Time = new (06, 50), Milligrays = 133 },
                new() { Id = 7, Date = new (2024, 10, 09), Time = new (03, 00), Milligrays = 160 },
                new() { Id = 8, Date = new (2024, 10, 09), Time = new (06, 20), Milligrays = 150 },
                new() { Id = 9, Date = new (2024, 10, 09), Time = new (04, 00), Milligrays = 190 },
                new() { Id = 10, Date = new (2024, 10, 09), Time = new (02, 10), Milligrays = 160 },
            };


            var radiationMeasurementsSameMonth = new List<RadiationMeasurements>()
            {
                new() { Id = 1, Date = new (2024, 10, 09), Time = new (6, 30), Milligrays = 120 },
                new() { Id = 2, Date = new (2024, 10, 02), Time = new (21, 00), Milligrays = 150 },
                new() { Id = 3, Date = new (2024, 10, 11), Time = new (11, 07), Milligrays = 120 },
                new() { Id = 4, Date = new (2024, 10, 20), Time = new (08, 02), Milligrays = 130 },
                new() { Id = 5, Date = new (2024, 10, 21), Time = new (02, 03), Milligrays = 110 },
                new() { Id = 6, Date = new (2024, 10, 06), Time = new (05, 30), Milligrays = 150 },
                new() { Id = 7, Date = new (2024, 10, 02), Time = new (07, 02), Milligrays = 130 },
                new() { Id = 8, Date = new (2024, 10, 01), Time = new (08, 30), Milligrays = 120 },
            };


            var radiationMeasurementsSameYear = new List<RadiationMeasurements>()
            {
                new() { Id = 1, Date = new (2025, 05, 20), Time = new (6, 30), Milligrays = 120 },
                new() { Id = 2, Date = new (2025, 06, 09), Time = new (11, 07), Milligrays = 110 },
                new() { Id = 3, Date = new (2025, 07, 10), Time = new (12, 00), Milligrays = 160 },
                new() { Id = 4, Date = new (2025, 08, 12), Time = new (13, 10), Milligrays = 140 },
                new() { Id = 5, Date = new (2025, 09, 02), Time = new (14, 20), Milligrays = 130 },
                new() { Id = 6, Date = new (2025, 10, 01), Time = new (09, 30), Milligrays = 120 },
                new() { Id = 7, Date = new (2025, 11, 26), Time = new (03, 00), Milligrays = 110 },
                new() { Id = 8, Date = new (2025, 12, 30), Time = new (02, 00), Milligrays = 150 },
            };


            mock.Setup(m => m.GetRadiationMeasurementsSumForTheDayAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                radiationMeasurementsSameDay.GroupBy(t => t.Time.Hour).Select(r =>
                    new RadiationMeasurementsTimeSum { Time = new TimeOnly(r.Key, 00), TotalMilligrays = r.Sum(a => a.Milligrays) }));
            
            mock.Setup(m => m.GetRadiationMeasurementsSumForTheMonthAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                radiationMeasurementsSameMonth.GroupBy(t => t.Date.Day).Select(r =>
                    new RadiationMeasurementsDateSum { Date = new DateOnly(date.Year, date.Month, r.Key), TotalMilligrays = r.Sum(a => a.Milligrays) }));
            
            mock.Setup(m => m.GetRadiationMeasurementsSumForTheYearAsync(It.IsAny<DateOnly>())).ReturnsAsync((DateOnly date) =>
                radiationMeasurementsSameYear.GroupBy(t => t.Date.Month).Select(r =>
                    new RadiationMeasurementsDateSum { Date = new DateOnly(date.Year, r.Key, 01), TotalMilligrays = r.Sum(a => a.Milligrays) }));


            return mock;
        }
    }
}
