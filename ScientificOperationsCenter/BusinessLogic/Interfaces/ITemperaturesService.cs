using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface ITemperaturesService
    {
        IEnumerable<TemperaturesTimeAverage> GetAverageTemperaturesForTheDay(DateOnly date);


        IEnumerable<TemperaturesDateAverage> GetAverageTemperaturesForTheMonth(DateOnly date);


        IEnumerable<TemperaturesDateAverage> GetAverageTemperaturesForTheYear(DateOnly date);
    }
}
