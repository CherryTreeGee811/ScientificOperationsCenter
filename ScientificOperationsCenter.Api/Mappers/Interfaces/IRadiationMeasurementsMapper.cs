using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Mappers.Interfaces
{
    /// <summary>
    /// Defines the contract for mapping radiation measurement data to view models.
    /// </summary>
    public interface IRadiationMeasurementsMapper
    {
        /// <summary>
        /// Retrieves and organizes by hour radiation measurements for a specific day and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="RadiationMeasurementsTimeViewModel"/> representing the total radiation
        /// for each hour of the specified day. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by hour in ascending order (e.g., 00:00 to 23:00).
        /// </remarks>
        Task<IEnumerable<RadiationMeasurementsTimeViewModel>> GetRadiationMeasurementsForTheDayAsync(DateOnly date);


        /// <summary>
        /// Retrieves and organizes by day radiation measurements for a specific month and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="RadiationMeasurementsDateViewModel"/> representing the total radiation
        /// for each day of the specified month. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by day in ascending order (e.g., 1st to 31st).
        /// </remarks>
        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheMonthAsync(DateOnly date);


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
        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheYearAsync(DateOnly date);
    }
}
