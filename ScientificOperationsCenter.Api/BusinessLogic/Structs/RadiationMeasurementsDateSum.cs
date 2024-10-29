namespace ScientificOperationsCenter.Api.BusinessLogic.Structs
{
    /// <summary>
    /// Represents the total radiation measurements for a specific date.
    /// </summary>
    public struct RadiationMeasurementsDateSum
    {
        /// <summary>
        /// Gets the date for which the total milligrays are calculated.
        /// </summary>
        public DateOnly Date { get; set; }


        /// <summary>
        /// Gets the total milligrays recorded for the specified date.
        /// </summary>
        public int TotalMilligrays { get; set; }
    }
}
