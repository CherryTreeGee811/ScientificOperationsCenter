using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.CustomExceptions;


namespace ScientificOperationsCenter.Api.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the contract for services that handle temperature data.
    /// </summary>
    public interface ITemperaturesService
    {
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
        Task<IEnumerable<TemperaturesTimeAverage>> GetAverageTemperaturesForTheDayAsync(DateOnly date);


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
        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheMonthAsync(DateOnly date);


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
        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheYearAsync(DateOnly date);
    }
}
