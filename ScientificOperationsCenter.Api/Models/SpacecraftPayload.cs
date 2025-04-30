using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ScientificOperationsCenter.Api.Models
{
    public class SpacecraftPayload
    {
        /// <summary>
        /// Gets or sets the unique identifier for the radiation measurement.
        /// </summary>
        [JsonProperty("dateTime")]
        public string? DateTime { get; set; }


        /// <summary>
        /// Gets or sets data type from Ground Station e.g., TemperatureReading or RadiationReading
        /// </summary>
        [Required]
        [JsonProperty("dataType")]
        public string? DataType { get; set; }


        /// <summary>
        /// Gets or sets temperature or radiation value from Ground Station
        /// </summary>
        [Required]
        [JsonProperty("data")]
        public string? Data { get; set; }


        /// <summary>
        /// Gets or sets string for cyclic redundancy check
        /// </summary>
        [JsonProperty("crc")]
        public string? CRC { get; set; }
    }
}
