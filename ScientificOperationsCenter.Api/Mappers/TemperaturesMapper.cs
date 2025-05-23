﻿using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Mappers
{
    /// <summary>
    /// Maps temperature data to view models for presentation.
    /// This class is responsible for transforming and organizing data retrieved from the service layer
    /// into view models that can be used by the presentation layer.
    /// </summary>
    /// <param name="temperaturesService">The service used to retrieve temperature data.</param>
    public sealed class TemperaturesMapper(
            ITemperaturesService temperaturesService
        ) : ITemperaturesMapper
    {
        /// <summary>
        /// Dependency on the temperatures service to fetch data.
        /// This service provides methods to retrieve average temperature data for specific time periods.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="temperaturesService"/> is null.</exception>
        private readonly ITemperaturesService _temperaturesService = temperaturesService
            ?? throw new ArgumentNullException(nameof(temperaturesService));


        /// <summary>
        /// Retrieves and organizes by hour temperature data for a specific day and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="TemperaturesViewModel"/> representing the average temperature
        /// for each hour of the specified day. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by hour in ascending order (e.g., 00:00 to 23:00).
        /// </remarks>
        public async Task<IEnumerable<TemperaturesViewModel>> GetTemperaturesForTheDayAsync(DateOnly date)
        {
            var temperatures = await _temperaturesService.GetAverageTemperaturesForTheDayAsync(date);
            if (temperatures.Any())
            {
                var values = temperatures.OrderBy(t => t.Time.Hour).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Time.ToString(), AverageTemperature = t.AverageTemperature });
                return values;
            }
            return [];
        }


        /// <summary>
        /// Retrieves and organizes by day temperature data for a specific month and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="TemperaturesViewModel"/> representing the average temperature
        /// for each day of the specified month. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by day in ascending order (e.g., 1st to 31st).
        /// </remarks>
        public async Task<IEnumerable<TemperaturesViewModel>> GetTemperaturesForTheMonthAsync(DateOnly date)
        {
            var temperatures = await _temperaturesService.GetAverageTemperaturesForTheMonthAsync(date);
            if (temperatures.Any()) 
            { 
                var values = temperatures.OrderBy(t => t.Date.Day).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Date.Day.ToString(), AverageTemperature = t.AverageTemperature });
                return values;
            }
            return [];
        }


        /// <summary>
        /// Retrieves and organizes by month temperature data for a specific year and maps them to view models.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// An ordered enumerable of <see cref="TemperaturesViewModel"/> representing the average temperature
        /// for each month of the specified year. If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// The returned collection is ordered by month in ascending order (e.g., January to December).
        /// </remarks>
        public async Task<IEnumerable<TemperaturesViewModel>> GetTemperaturesForTheYearAsync(DateOnly date)
        {
            var temperatures = await _temperaturesService.GetAverageTemperaturesForTheYearAsync(date);
            if (temperatures.Any())
            {
                var values = temperatures.OrderBy(t => t.Date.Month).Select(t =>
                    new TemperaturesViewModel { TimeFrame = t.Date.ToString("MMMM"), AverageTemperature = t.AverageTemperature });
                return values;
            }
            return [];
        }
    }
}