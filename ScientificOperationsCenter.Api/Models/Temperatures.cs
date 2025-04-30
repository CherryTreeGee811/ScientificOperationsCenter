using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScientificOperationsCenter.Api.Models
{
    /// <summary>
    /// Represents a temperature record with a specific date and time.
    /// </summary>
    public class Temperatures
    {
        /// <summary>
        /// Gets or sets the unique identifier for the temperature record.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the date when the temperature was recorded.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }


        /// <summary>
        /// Gets or sets the time when the temperature was recorded.
        /// </summary>
        [Required]
        public TimeOnly Time { get; set; }


        /// <summary>
        /// Gets or sets the temperature in degrees Celsius.
        /// </summary>
        [Required]
        [Range(-10000, 10000)]
        public int TemperatureCelcius { get; set; }
    }
}