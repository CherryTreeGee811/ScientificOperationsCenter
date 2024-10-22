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


        public async Task<IEnumerable<TemperaturesTimeViewModel>> GetTemperaturesForTheDayAsync(DateOnly date)
        {
            var temperatures = await _service.GetAverageTemperaturesForTheDayAsync(date);
            if (temperatures.Any())
            {
                var values = temperatures.OrderBy(t => t.Time.Hour).Select(t =>
                    new TemperaturesTimeViewModel { Hour = t.Time, AverageTemperature = t.AverageTemperature });
                return values;
            }
            return Enumerable.Empty<TemperaturesTimeViewModel>();
        }


        public async Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheMonthAsync(DateOnly date)
        {
            var temperatures = await _service.GetAverageTemperaturesForTheMonthAsync(date);
            if (temperatures.Any()) 
            { 
                var values = temperatures.OrderBy(t => t.Date.Day).Select(t =>
                    new TemperaturesDateViewModel { Date = t.Date.Day.ToString(), AverageTemperature = t.AverageTemperature });
                return values;
            }
            return Enumerable.Empty<TemperaturesDateViewModel>();
        }


        public async Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheYearAsync(DateOnly date)
        {
            var temperatures = await _service.GetAverageTemperaturesForTheYearAsync(date);
            if (temperatures.Any())
            {
                var values = temperatures.OrderBy(t => t.Date.Month).Select(t =>
                    new TemperaturesDateViewModel { Date = t.Date.ToString("MMMM"), AverageTemperature = t.AverageTemperature });
                return values;
            }
            return Enumerable.Empty<TemperaturesDateViewModel>();
        }
    }
}