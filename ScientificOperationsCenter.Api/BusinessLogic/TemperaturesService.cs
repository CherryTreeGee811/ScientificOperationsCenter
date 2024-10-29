using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using Serilog;


namespace ScientificOperationsCenter.Api.BusinessLogic
{
    /// <summary>
    /// Provides services for retrieving and processing temperature data.
    /// This includes filtering temperatures by day, month, and year,
    /// grouping them by hour for daily data, by day for monthly data,
    /// and calculating average degrees Celsius for specified time periods.
    /// </summary>
    public sealed class TemperaturesService : ITemperaturesService
    {
        /// <summary>
        /// Provides access to functions in the TemperaturesRepository.
        /// </summary>
        private readonly ITemperaturesRepository _temperaturesRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="TemperaturesService"/> class.
        /// </summary>
        /// <param name="temperaturesRepository">The repository used to access temperature data.</param>
        public TemperaturesService(ITemperaturesRepository temperaturesRepository)
        {
            _temperaturesRepository = temperaturesRepository;
        }


        /// <summary>
        /// Retrieves average temperatures for each hour of the specified day.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TemperaturesTimeAverage"/> representing the average temperature 
        /// for each hour of the specified day. If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<TemperaturesTimeAverage>> GetAverageTemperaturesForTheDayAsync(DateOnly date)
        {
            
            var temperatures = await _temperaturesRepository.GetByDayAsync(date);
            if (!temperatures.Any())
            {
                return Enumerable.Empty<TemperaturesTimeAverage>();
            }
            try
            {
                var values = temperatures
                    .GroupBy(t => t.Time.Hour)
                    .Select(t => new TemperaturesTimeAverage {
                        Time = new TimeOnly(t.Key, 00),
                        AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius)) 
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheDayAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }   
        }


        /// <summary>
        /// Retrieves average temperatures for each day of the specified month.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TemperaturesDateAverage"/> representing the average temperature 
        /// for each day of the specified month. If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheMonthAsync(DateOnly date)
        {
            var temperatures = await _temperaturesRepository.GetByMonthAsync(date);
            if (!temperatures.Any())
            {
                return Enumerable.Empty<TemperaturesDateAverage>();
            }
            try
            {
                var values = temperatures
                    .GroupBy(t => t.Date.Day)
                    .Select(t => new TemperaturesDateAverage {
                        Date = new DateOnly(date.Year, date.Month, t.Key),
                        AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius))
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheMonthAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }   
        }


        /// <summary>
        /// Retrieves average temperatures for each month of the specified year.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TemperaturesDateAverage"/> representing the average temperature 
        /// for each month of the specified year. If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheYearAsync(DateOnly date)
        {
            var temperatures = await _temperaturesRepository.GetByYearAsync(date);
            if (!temperatures.Any())
            {
                return Enumerable.Empty<TemperaturesDateAverage>();
            }
            try
            {
                var values = temperatures
                    .GroupBy(t => t.Date.Month)
                    .Select(t => new TemperaturesDateAverage {
                        Date = new DateOnly(date.Year, t.Key, 01),
                        AverageTemperature = (int)Math.Round(t.Average(a => a.TemperatureCelcius))
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesService -> GetAverageTemperaturesForTheYearAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }
        }
    }
}