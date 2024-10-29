namespace ScientificOperationsCenter.Api.ViewModels
{
    /// <summary>
    /// Represents the total radiation exposure in milligrays for a specific hour.
    /// </summary>
    public class RadiationMeasurementsTimeViewModel
    {
        /// <summary>
        /// Gets or sets the hour of the day (e.g., 7:00 AM).
        /// </summary>
        public TimeOnly Hour { get; set; }


        /// <summary>
        /// Gets or sets the total radiation exposure for that hour in milligrays.
        /// </summary>
        public int TotalRadiation { get; set; }
    }
}
