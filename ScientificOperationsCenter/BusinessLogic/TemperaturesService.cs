using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.DAL.Interfaces;


namespace ScientificOperationsCenter.BusinessLogic
{
    public class TemperaturesService : ITemperaturesService
    {

        private ITemperaturesRepository _temperaturesRepository;


        public TemperaturesService(ITemperaturesRepository temperaturesRepository)
        {
            _temperaturesRepository = temperaturesRepository;
        }


        public IEnumerable<TemperatureTimeAverages> GetAverageTemperaturesForTheDay(DateOnly date)
        {
            var temperatures = _temperaturesRepository.GetByDay(date);
            IEnumerable<TemperatureTimeAverages> values = temperatures.GroupBy(t => t.Time.Hour)
                .Select(t => new TemperatureTimeAverages { Time = new TimeOnly(t.Key, 00), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) });

            return values;
        }


        public IEnumerable<TemperatureDateAverages> GetAverageTemperaturesForTheMonth(DateOnly date)
        {
            var temperatures = _temperaturesRepository.GetByMonth(date);
            IEnumerable<TemperatureDateAverages> values = temperatures.GroupBy(t => t.Date.Day)
                .Select(t => new TemperatureDateAverages { Date = new DateOnly(date.Year, date.Month, t.Key), AverageTemperature = (int) Math.Round(t.Average(a => a.TemperatureCelcius)) });
            return values;
        }


        public IEnumerable<TemperatureDateAverages> GetAverageTemperaturesForTheYear(DateOnly date)
        {
            var temperatures = _temperaturesRepository.GetByYear(date);
            IEnumerable<TemperatureDateAverages> values = temperatures.GroupBy(t => t.Date.Month)
                .Select(t => new TemperatureDateAverages { Date = new DateOnly(date.Year, t.Key, 01), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) });
            return values;
        }
    }
}
