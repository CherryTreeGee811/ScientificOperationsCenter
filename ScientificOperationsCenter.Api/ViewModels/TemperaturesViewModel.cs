using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ScientificOperationsCenter.Api.ViewModels
{
    /// <summary>
    /// Represents the average temperature in degrees Celcius for a specific hour, day, or month.
    /// </summary>
    public class TemperaturesViewModel
    {
        /// <summary>
        /// Gets or sets the date, which can be the name of a month (e.g. January) or a day number (e.g. 15), or hour (e.g. 7:00 AM)..
        /// </summary>
        [Required]
        [JsonProperty("timeFrame")]
        public string? TimeFrame { get; set; }


        /// <summary>
        /// Gets or sets the average temperature for the day or month in degrees Celsius.
        /// </summary>
        [Required]
        [JsonProperty("averageTemperature")]
        public int AverageTemperature { get; set; }
    }
}
