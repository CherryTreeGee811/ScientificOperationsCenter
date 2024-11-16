using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScientificOperationsCenter.Api.Models
{
    /// <summary>
    /// Represents a radiation measurement record.
    /// </summary>
    public class RadiationMeasurements
    {
        /// <summary>
        /// Gets or sets the unique identifier for the radiation measurement.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the date of the radiation measurement.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }


        /// <summary>
        /// Gets or sets the time of the radiation measurement.
        /// </summary>
        [Required]
        public TimeOnly Time { get; set; }


        /// <summary>
        /// Gets or sets the amount of radiation measured in milligrays.
        /// </summary>
        [Required]
        public int Milligrays { get; set; }
    }
}