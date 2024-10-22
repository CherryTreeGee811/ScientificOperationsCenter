using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface ITemperaturesService
    {
        Task<IEnumerable<TemperaturesTimeAverage>> GetAverageTemperaturesForTheDayAsync(DateOnly date);


        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheMonthAsync(DateOnly date);


        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheYearAsync(DateOnly date);
    }
}
