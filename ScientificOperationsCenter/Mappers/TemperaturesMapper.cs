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


        public IEnumerable<TemperaturesTimeViewModel> GetTemperaturesForTheDay(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheDay(date);
            var values = temperatures.OrderBy(t => t.Time.Hour).Select(t =>
                new TemperaturesTimeViewModel { Hour = t.Time, AverageTemperature = t.AverageTemperature });
            return values;
        }


        public IEnumerable<TemperaturesDateViewModel> GetTemperaturesForTheMonth(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheMonth(date);
            var values = temperatures.OrderBy(t => t.Date.Day).Select(t =>
                new TemperaturesDateViewModel { Date = t.Date.Day.ToString(), AverageTemperature = t.AverageTemperature });
            return values;
        }


        public IEnumerable<TemperaturesDateViewModel> GetTemperaturesForTheYear(DateOnly date)
        {
            var temperatures = _service.GetAverageTemperaturesForTheYear(date);
            var values = temperatures.OrderBy(t => t.Date.Month).Select(t =>
                new TemperaturesDateViewModel { Date = t.Date.ToString("MMMM"), AverageTemperature = t.AverageTemperature });
            return values;
        }
    }
}