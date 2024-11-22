using ScientificOperationsCenter.Api.Models;
using ScientificOperationsCenter.Api.CustomExceptions;


namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    // ToDo: Update comments


    /// <summary>
    /// Defines the contract for accessing radiation measurement data.
    /// </summary>
    public interface IRadiationMeasurementsRepository
    {
        /// <summary>
        /// Retrieves all radiation measurements from the database for a specific day,
        /// where the year, month, and day match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified day. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<RadiationMeasurements>> GetByDayAsync(DateOnly date);


        /// <summary>
        /// Retrieves all radiation measurements from the database for a specific month,
        /// where the year and month match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified month. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<RadiationMeasurements>> GetByMonthAsync(DateOnly date);


        /// <summary>
        /// Retrieves all radiation measurements from the database for the year 
        /// corresponding to the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified year. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<RadiationMeasurements>> GetByYearAsync(DateOnly date);


        Task AddRadiationMeasurements(RadiationMeasurements[] radiationMeasurementsList);


        Task AddRadiationMeasurement(RadiationMeasurements radiationMeasurement);
    }
}
