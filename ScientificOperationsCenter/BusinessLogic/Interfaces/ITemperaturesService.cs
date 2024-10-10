using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface ITemperaturesService
    {
        IEnumerable<TemperatureTimeAverages> GetAverageTemperaturesForTheDay(DateOnly date);


        IEnumerable<TemperatureDateAverages> GetAverageTemperaturesForTheMonth(DateOnly date);


        IEnumerable<TemperatureDateAverages> GetAverageTemperaturesForTheYear(DateOnly date);
    }
}
