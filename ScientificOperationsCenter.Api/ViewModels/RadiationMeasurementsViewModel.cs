using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ScientificOperationsCenter.Api.ViewModels
{
    /// <summary>
    /// Represents the total radiation exposure in milligrays for a specific hour, day, or month.
    /// </summary>
    public class RadiationMeasurementsViewModel
    {
        /// <summary>
        /// Gets or sets the date or time, which can be the name of a month (e.g. January), a day number (e.g. 15), or hour (e.g. 7:00 AM).
        /// </summary>
        [Required]
        [JsonProperty("timeFrame")]
        public string? TimeFrame { get; set; }


        /// <summary>
        /// Gets or sets the total radiation exposure for the day or month in milligrays.
        /// </summary>
        [Required]
        [JsonProperty("totalRadiation")]
        public int TotalRadiation { get; set; }
    }
}
