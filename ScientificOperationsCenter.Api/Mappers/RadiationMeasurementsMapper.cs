using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Mappers
{
    /// <summary>
    /// Maps radiation measurement data to view models for presentation.
    /// This class is responsible for transforming and organizing data retrieved from the service layer
    /// into view models that can be used by the presentation layer.
    /// </summary>
    /// <param name="radiationMeasurementsService">The service used to retrieve radiation measurement data.</param>
    public sealed class RadiationMeasurementsMapper(
            IRadiationMeasurementsService radiationMeasurementsService
        ) : IRadiationMeasurementsMapper
    {
        /// <summary>
        /// Dependency on the radiation measurements service to fetch data.
        /// This service provides methods to retrieve total radiation measurement data for specific time periods.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="radiationMeasurementsService"/> is null.</exception>
        private readonly IRadiationMeasurementsService _radiationMeasurementsService = radiationMeasurementsService
            ?? throw new ArgumentNullException(nameof(radiationMeasurementsService));


        /// <summary>
        /// Retrieves and organizes by hour radiation measurements for a specific day and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="RadiationMeasurementsViewModel"/> representing the total radiation
        /// for each hour of the specified day. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by hour in ascending order (e.g., 00:00 to 23:00).
        /// </remarks>
        public async Task<IEnumerable<RadiationMeasurementsViewModel>> GetRadiationMeasurementsForTheDayAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheDayAsync(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Time.Hour).Select(r =>
                    new RadiationMeasurementsViewModel { TimeFrame = r.Time.ToString(), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return [];
        }


        /// <summary>
        /// Retrieves and organizes by day radiation measurements for a specific month and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="RadiationMeasurementsViewModel"/> representing the total radiation
        /// for each day of the specified month. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by day in ascending order (e.g., 1st to 31st).
        /// </remarks>
        public async Task<IEnumerable<RadiationMeasurementsViewModel>> GetRadiationMeasurementsForTheMonthAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheMonthAsync(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Date.Day).Select(r =>
                    new RadiationMeasurementsViewModel { TimeFrame = r.Date.Day.ToString(), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return [];
        }


        /// <summary>
        /// Retrieves and organizes by month radiation measurements for a specific year and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="RadiationMeasurementsDateViewModel"/> representing the total radiation
        /// for each month of the specified year. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by month in ascending order (e.g., January to December).
        /// </remarks>
        public async Task<IEnumerable<RadiationMeasurementsViewModel>> GetRadiationMeasurementsForTheYearAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsService.GetRadiationMeasurementsSumForTheYearAsync(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Date.Month).Select(r =>
                    new RadiationMeasurementsViewModel { TimeFrame = r.Date.ToString("MMMM"), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return [];
        }
    }
}
