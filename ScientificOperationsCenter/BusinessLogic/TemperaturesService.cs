using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.DAL.Interfaces;


namespace ScientificOperationsCenter.BusinessLogic
{
    public sealed class TemperaturesService : ITemperaturesService
    {

        private readonly ITemperaturesRepository _temperaturesRepository;


        public TemperaturesService(ITemperaturesRepository temperaturesRepository)
        {
            _temperaturesRepository = temperaturesRepository;
        }


        public IEnumerable<TemperaturesTimeAverage> GetAverageTemperaturesForTheDay(DateOnly date)
        {
            
            var temperatures = _temperaturesRepository.GetByDay(date);
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
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesTimeAverage>();
        }


        public IEnumerable<TemperaturesDateAverage> GetAverageTemperaturesForTheMonth(DateOnly date)
        {
            var temperatures = _temperaturesRepository.GetByMonth(date);
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
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesDateAverage>();
        }


        public IEnumerable<TemperaturesDateAverage> GetAverageTemperaturesForTheYear(DateOnly date)
        {
            var temperatures = _temperaturesRepository.GetByYear(date);
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
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<TemperaturesDateAverage>();
        }
    }
}
