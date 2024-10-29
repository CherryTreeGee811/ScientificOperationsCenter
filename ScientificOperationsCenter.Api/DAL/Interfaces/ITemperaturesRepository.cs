using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    /// <summary>
    /// Defines the contract for accessing temperature data.
    /// </summary>
    public interface ITemperaturesRepository
    {
        /// <summary>
        /// Retrieves all temperature records from the database for a specific day,
        /// where the year, month, and day match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified day. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<Temperatures>> GetByDayAsync(DateOnly date);


        /// <summary>
        /// Retrieves all temperature records from the database for a specific month,
        /// where the year and month match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified month. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<Temperatures>> GetByMonthAsync(DateOnly date);


        /// <summary>
        /// Retrieves all temperature records from the database for a specific year,
        /// where the year matches the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified year. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
        Task<IEnumerable<Temperatures>> GetByYearAsync(DateOnly date);
    }
}
