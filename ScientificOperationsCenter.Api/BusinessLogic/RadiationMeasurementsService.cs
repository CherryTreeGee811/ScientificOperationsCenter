using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using Serilog;


namespace ScientificOperationsCenter.Api.BusinessLogic
{
    /// <summary>
    /// Provides services for retrieving and processing radiation measurement data.
    /// This includes filtering measurements by day, month, and year,
    /// grouping them by hour for daily data, by day for monthly data,
    /// and calculating total milligrays for specified time periods.
    /// </summary>
    public sealed class RadiationMeasurementsService : IRadiationMeasurementsService
    {
        /// <summary>
        /// Provides access to functions in the RadiationMeasurementsRepository.
        /// </summary>
        private readonly IRadiationMeasurementsRepository _radiationMeasurementsRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="RadiationMeasurementsService"/> class.
        /// </summary>
        /// <param name="radiationMeasurementsRepository">The repository used to access radiation measurement data.</param>
        public RadiationMeasurementsService(IRadiationMeasurementsRepository radiationMeasurementsRepository)
        {
            _radiationMeasurementsRepository = radiationMeasurementsRepository;
        }


        /// <summary>
        /// Retrieves data filtered by day from the repository.
        /// Groups radiation measurement data by hour and calculates the total milligrays for each hour of the day.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="RadiationMeasurementsTimeSum"/> representing the total milligrays 
        /// for each hour of the specified day. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<RadiationMeasurementsTimeSum>> GetRadiationMeasurementsSumForTheDayAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByDayAsync(date);
            if (!radiationMeasurements.Any())
            {
                return Enumerable.Empty<RadiationMeasurementsTimeSum>();
            }
            try
            {
                var values = radiationMeasurements
                    .GroupBy(t => t.Time.Hour)
                    .Select(r => new RadiationMeasurementsTimeSum {
                        Time = new TimeOnly(r.Key, 00),
                        TotalMilligrays = r.Sum(a => a.Milligrays) 
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsService -> GetRadiationMeasurementsSumForTheDayAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }       
        }


        /// <summary>
        /// Retrieves data filtered by month from the repository.
        /// Groups radiation measurement data by day and calculates the total milligrays for each day of the month.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="RadiationMeasurementsDateSum"/> representing the total milligrays 
        /// for each day of the specified month. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheMonthAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByMonthAsync(date);
            if (!radiationMeasurements.Any())
            {
                return Enumerable.Empty<RadiationMeasurementsDateSum>();
            }
            try
            {
                var values = radiationMeasurements
                    .GroupBy(t => t.Date.Day)
                    .Select(r => new RadiationMeasurementsDateSum { 
                        Date = new DateOnly(date.Year, date.Month, r.Key),
                        TotalMilligrays = r.Sum(a => a.Milligrays) 
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsService -> GetRadiationMeasurementsSumForTheMonthAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }
        }


        /// <summary>
        /// Retrieves radiation measurement data filtered by year from the repository.
        /// Groups radiation measurement data by month and calculates the total milligrays for each month of the year.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An enumerable collection of <see cref="RadiationMeasurementsDateSum"/> representing the total milligrays 
        /// for each month of the specified year. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// Thrown when an unexpected error occurs while processing the data.
        /// </exception>
        public async Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheYearAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByYearAsync(date);
            if (!radiationMeasurements.Any())
            {
                return Enumerable.Empty<RadiationMeasurementsDateSum>();
            }
            try
            {
                var values = radiationMeasurements
                    .GroupBy(t => t.Date.Month)
                    .Select(r => new RadiationMeasurementsDateSum {
                        Date = new DateOnly(date.Year, r.Key, 01), 
                        TotalMilligrays = r.Sum(a => a.Milligrays) 
                    })
                    .ToList();

                return values;
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsService -> GetRadiationMeasurementsSumForTheYearAsync().");
                throw new BusinessLogicException("An unexpected error occurred.", gEx);
            }   
        }
    }
}