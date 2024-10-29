namespace ScientificOperationsCenter.Api.ViewModels
{
    /// <summary>
    /// Represents the total radiation exposure in milligrays for a specific day or month.
    /// </summary>
    public class RadiationMeasurementsDateViewModel
    {
        /// <summary>
        /// Gets or sets the date, which can be the name of a month (e.g. January) or a day number (e.g. 15).
        /// </summary>
        public string Date { get; set; }


        /// <summary>
        /// Gets or sets the total radiation exposure for the day or month in milligrays.
        /// </summary>
        public int TotalRadiation { get; set; }
    }
}
