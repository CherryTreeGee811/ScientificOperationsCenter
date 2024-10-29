namespace ScientificOperationsCenter.Api.BusinessLogic.Structs
{
    public struct TemperaturesTimeAverage
    {
        /// <summary>
        /// Gets or sets the time for which the average temperature is calculated.
        /// </summary>
        public TimeOnly Time;

        /// <summary>
        /// Gets or sets the average temperature recorded for the specified time.
        /// </summary>
        public int AverageTemperature;
    }
}
