namespace ScientificOperationsCenter.Api.BusinessLogic.Structs
{
    /// <summary>
    /// Represents the total radiation measurements for a specific time.
    /// </summary>
    public struct RadiationMeasurementsTimeSum
    {
        /// <summary>
        /// Gets or sets the time of the radiation measurement.
        /// </summary>
        public TimeOnly Time { get; set; }


        /// <summary>
        /// Gets or sets the total amount of radiation measured in milligrays.
        /// </summary>
        public int TotalMilligrays { get; set; }
    }
}
