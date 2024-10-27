using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.DAL.Interfaces;
using Serilog;

namespace ScientificOperationsCenter.BusinessLogic
{
    public sealed class TemperaturesService : ITemperaturesService
    {

        private readonly ITemperaturesRepository _temperaturesRepository;


        public TemperaturesService(ITemperaturesRepository temperaturesRepository)
        {
            _temperaturesRepository = temperaturesRepository;
        }


        public async Task<IEnumerable<TemperaturesTimeAverage>> GetAverageTemperaturesForTheDayAsync(DateOnly date)
        {
            
            var temperatures = await _temperaturesRepository.GetByDayAsync(date);
            if (temperatures.Any())
            {
                try
                {
                    var values = temperatures.GroupBy(t => t.Time.Hour)
                        .Select(t => new TemperaturesTimeAverage { Time = new TimeOnly(t.Key, 00), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) });
                    return values;
                }
                catch (Exception gEx)
                {
                    Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheDayAsync().");
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesTimeAverage>();
        }


        public async Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheMonthAsync(DateOnly date)
        {
            var temperatures = await _temperaturesRepository.GetByMonthAsync(date);
            if (temperatures.Any())
            {
                try
                {
                    var values = temperatures.GroupBy(t => t.Date.Day)
                        .Select(t => new TemperaturesDateAverage { Date = new DateOnly(date.Year, date.Month, t.Key), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) });
                    return values;
                }
                catch (Exception gEx)
                {
                    Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheMonthAsync().");
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesDateAverage>();
        }


        public async Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheYearAsync(DateOnly date)
        {
            var temperatures = await _temperaturesRepository.GetByYearAsync(date);
            if (temperatures.Any())
            {
                try
                {
                    var values = temperatures.GroupBy(t => t.Date.Month)
                        .Select(t => new TemperaturesDateAverage { Date = new DateOnly(date.Year, t.Key, 01), AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) });
                    return values;
                }
                catch (Exception gEx)
                {
                    Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheYearAsync().");
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesDateAverage>();
        }
    }
}