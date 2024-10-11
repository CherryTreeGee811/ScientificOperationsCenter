using Moq;
using ScientificOperationsCenter.DAL.Interfaces;
using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.Tests.Mocks
{
    internal class MockITemperaturesRepository
    {
        public static Mock<ITemperaturesRepository> GetMock()
        {
            var mock = new Mock<ITemperaturesRepository>();


            var temperatures = new List<Temperatures>()
            {
                new Temperatures { Id = 1, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), TemperatureCelcius = -4 },
                new Temperatures { Id = 2, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), TemperatureCelcius = 12 },
                new Temperatures { Id = 3, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), TemperatureCelcius = 8 },
                new Temperatures { Id = 4, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(04, 30), TemperatureCelcius = 5 },
                new Temperatures { Id = 5, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), TemperatureCelcius = 12 },
                new Temperatures { Id = 6, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(05, 30), TemperatureCelcius = 15 },
                new Temperatures { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), TemperatureCelcius = 10 },
                new Temperatures { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), TemperatureCelcius = 9 },
                new Temperatures { Id = 9, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), TemperatureCelcius = -2 },
                new Temperatures { Id = 10, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(03, 30), TemperatureCelcius = 5 }
            };


            mock.Setup(m => m.GetByDay(It.IsAny<DateOnly>())).Returns((DateOnly date) => temperatures.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day) );
            mock.Setup(m => m.GetByMonth(It.IsAny<DateOnly>())).Returns((DateOnly date) => temperatures.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month));
            mock.Setup(m => m.GetByYear(It.IsAny<DateOnly>())).Returns((DateOnly date) => temperatures.Where(x => x.Date.Year == date.Year));


            return mock;
        }
    }
}
