using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.CustomExceptions;


namespace ScientificOperationsCenter.Api.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the contract for services that handle radiation measurement data.
    /// </summary>
    public interface IRadiationMeasurementsService
    {
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
        Task<IEnumerable<RadiationMeasurementsTimeSum>> GetRadiationMeasurementsSumForTheDayAsync(DateOnly date);


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
        Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheMonthAsync(DateOnly date);


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
        Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheYearAsync(DateOnly date);
    }
}
