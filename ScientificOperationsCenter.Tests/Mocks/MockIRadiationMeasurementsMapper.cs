using Moq;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.Mappers.Interfaces;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Tests.Mocks
{
    internal sealed class MockIRadiationMeasurementsMapper
    {
        public static Mock<IRadiationMeasurementsMapper> GetMock()
        {
            var mock = new Mock<IRadiationMeasurementsMapper>();


            var radiationMeasurementsSameDay = new List<RadiationMeasurementsTimeSum>()
            {
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(01, 00), TotalMilligrays = 430 },
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(6, 00), TotalMilligrays = 400 },
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(21, 00), TotalMilligrays = 110 },
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(03, 00), TotalMilligrays = 160 },
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(04, 00), TotalMilligrays = 190 },
                new RadiationMeasurementsTimeSum { Time = new TimeOnly(02, 00), TotalMilligrays = 160 }
            };


            var radiationMeasurementsSameMonth = new List<RadiationMeasurementsDateSum>()
            {
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 09), TotalMilligrays = 120 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 02), TotalMilligrays = 250 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 11), TotalMilligrays = 120 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 20), TotalMilligrays = 130 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 21), TotalMilligrays = 110 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 06), TotalMilligrays = 150 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2024, 10, 01), TotalMilligrays = 120 },
            };


            var radiationMeasurementsSameYear = new List<RadiationMeasurementsDateSum>()
            {
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 08, 01), TotalMilligrays = 140 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 05, 01), TotalMilligrays = 120 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 07, 01), TotalMilligrays = 160 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 11, 01), TotalMilligrays = 110 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 09, 01), TotalMilligrays = 130 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 10, 01), TotalMilligrays = 120 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 06, 01), TotalMilligrays = 110 },
                new RadiationMeasurementsDateSum { Date = new DateOnly(2025, 12, 01), TotalMilligrays = 150 },
            };


            mock.Setup(m => m.GetRadiationMeasurementsForTheDay(It.IsAny<DateOnly>())).Returns((DateOnly date) =>
                radiationMeasurementsSameDay.OrderBy(t => t.Time.Hour).Select(r =>
                    new RadiationMeasurementsTimeViewModel { Hour = r.Time, TotalRadiation = r.TotalMilligrays }));

            mock.Setup(m => m.GetRadiationMeasurementsForTheMonth(It.IsAny<DateOnly>())).Returns((DateOnly date) =>
                radiationMeasurementsSameMonth.OrderBy(t => t.Date.Day).Select(r =>
                    new RadiationMeasurementsDateViewModel { Date = r.Date.Day.ToString(), TotalRadiation = r.TotalMilligrays }));

            mock.Setup(m => m.GetRadiationMeasurementsForTheYear(It.IsAny<DateOnly>())).Returns((DateOnly date) =>
                radiationMeasurementsSameYear.OrderBy(t => t.Date.Month).Select(r =>
                    new RadiationMeasurementsDateViewModel { Date = r.Date.ToString("MMMM"), TotalRadiation = r.TotalMilligrays }));


            return mock;
        }
    }
}
