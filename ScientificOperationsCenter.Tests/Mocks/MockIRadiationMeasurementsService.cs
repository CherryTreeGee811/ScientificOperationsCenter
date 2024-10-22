using Moq;
using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.Tests.Mocks
{
    internal sealed class MockIRadiationMeasurementsService
    {
        public static Mock<IRadiationMeasurementsService> GetMock()
        {
            var mock = new Mock<IRadiationMeasurementsService>();


            var radiationMeasurementsSameDay = new List<RadiationMeasurements>()
            {
                new RadiationMeasurements { Id = 1, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(01, 00), Milligrays = 160 },
                new RadiationMeasurements { Id = 2, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(01, 20), Milligrays = 150 },
                new RadiationMeasurements { Id = 3, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(01, 50), Milligrays = 120 },
                new RadiationMeasurements { Id = 4, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(6, 30), Milligrays = 120 },
                new RadiationMeasurements { Id = 5, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 02), Milligrays = 110 },
                new RadiationMeasurements { Id = 6, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 50), Milligrays = 133 },
                new RadiationMeasurements { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(03, 00), Milligrays = 160 },
                new RadiationMeasurements { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 20), Milligrays = 150 },
                new RadiationMeasurements { Id = 9, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(04, 00), Milligrays = 190 },
                new RadiationMeasurements { Id = 10, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(02, 10), Milligrays = 160 },
            };


            var radiationMeasurementsSameMonth = new List<RadiationMeasurements>()
            {
                new RadiationMeasurements { Id = 1, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(6, 30), Milligrays = 120 },
                new RadiationMeasurements { Id = 2, Date = new DateOnly(2024, 10, 02), Time = new TimeOnly(21, 00), Milligrays = 150 },
                new RadiationMeasurements { Id = 3, Date = new DateOnly(2024, 10, 11), Time = new TimeOnly(11, 07), Milligrays = 120 },
                new RadiationMeasurements { Id = 4, Date = new DateOnly(2024, 10, 20), Time = new TimeOnly(08, 02), Milligrays = 130 },
                new RadiationMeasurements { Id = 5, Date = new DateOnly(2024, 10, 21), Time = new TimeOnly(02, 03), Milligrays = 110 },
                new RadiationMeasurements { Id = 6, Date = new DateOnly(2024, 10, 06), Time = new TimeOnly(05, 30), Milligrays = 150 },
                new RadiationMeasurements { Id = 7, Date = new DateOnly(2024, 10, 02), Time = new TimeOnly(07, 02), Milligrays = 130 },
                new RadiationMeasurements { Id = 8, Date = new DateOnly(2024, 10, 01), Time = new TimeOnly(08, 30), Milligrays = 120 },
            };


            var radiationMeasurementsSameYear = new List<RadiationMeasurements>()
            {
                new RadiationMeasurements { Id = 1, Date = new DateOnly(2025, 05, 20), Time = new TimeOnly(6, 30), Milligrays = 120 },
                new RadiationMeasurements { Id = 2, Date = new DateOnly(2025, 06, 09), Time = new TimeOnly(11, 07), Milligrays = 110 },
                new RadiationMeasurements { Id = 3, Date = new DateOnly(2025, 07, 10), Time = new TimeOnly(12, 00), Milligrays = 160 },
                new RadiationMeasurements { Id = 4, Date = new DateOnly(2025, 08, 12), Time = new TimeOnly(13, 10), Milligrays = 140 },
                new RadiationMeasurements { Id = 5, Date = new DateOnly(2025, 09, 02), Time = new TimeOnly(14, 20), Milligrays = 130 },
                new RadiationMeasurements { Id = 6, Date = new DateOnly(2025, 10, 01), Time = new TimeOnly(09, 30), Milligrays = 120 },
                new RadiationMeasurements { Id = 7, Date = new DateOnly(2025, 11, 26), Time = new TimeOnly(03, 00), Milligrays = 110 },
                new RadiationMeasurements { Id = 8, Date = new DateOnly(2025, 12, 30), Time = new TimeOnly(02, 00), Milligrays = 150 },
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
