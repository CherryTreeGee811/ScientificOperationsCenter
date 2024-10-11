using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Mappers.Interfaces;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Mappers
{
    public sealed class TemperaturesMapper : ITemperaturesMapper
    {
        private readonly ITemperaturesService _service;


        public TemperaturesMapper(ITemperaturesService service)
        {
            _service = service;
        }


        public IEnumerable<TemperatureTimeViewModel> GetTemperaturesForTheDay(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheDay(date);
            IEnumerable<TemperatureTimeViewModel> values = temperatures.OrderBy(t => t.Time.Hour).Select(t =>
                new TemperatureTimeViewModel { Hour = t.Time, AverageTemperature = t.AverageTemperature });
            return values;
        }


        public IEnumerable<TemperatureDateViewModel> GetTemperaturesForTheMonth(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheMonth(date);
            IEnumerable<TemperatureDateViewModel> values = temperatures.OrderBy(t => t.Date.Day).Select(t =>
                new TemperatureDateViewModel { Date = t.Date.Day.ToString(), AverageTemperature = t.AverageTemperature });
            return values;
        }


        public IEnumerable<TemperatureDateViewModel> GetTemperaturesForTheYear(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheYear(date);
            IEnumerable<TemperatureDateViewModel> values = temperatures.OrderBy(t => t.Date.Month).Select(t =>
                new TemperatureDateViewModel { Date = t.Date.ToString("MMMM"), AverageTemperature = t.AverageTemperature });
            return values;
        }
    }
}