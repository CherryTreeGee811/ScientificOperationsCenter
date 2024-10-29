namespace ScientificOperationsCenter.Api.ViewModels
{
    /// <summary>
    /// Represents the average temperature in degrees Celcius for a specific hour.
    /// </summary>
    public class TemperaturesTimeViewModel
    {
        /// <summary>
        /// Gets or sets the hour of the day (e.g. 7:00 AM).
        /// </summary>
        public TimeOnly Hour { get; set; }


        /// <summary>
        /// Gets or sets the average temperature for that hour in degrees Celsius.
        /// </summary>
        public int AverageTemperature { get; set; }
    }
}
