namespace ScientificOperationsCenter.Api.BusinessLogic.Structs
{
    /// <summary>
    /// Represents the average temperature recorded for a specific date.
    /// </summary>
    public struct TemperaturesDateAverage
    {
        /// <summary>
        /// Gets or sets the date for which the average temperature is calculated.
        /// </summary>
        public DateOnly Date;


        /// <summary>
        /// Gets or sets the average temperature recorded for the specified date.
        /// </summary>
        public int AverageTemperature;
    }
}
