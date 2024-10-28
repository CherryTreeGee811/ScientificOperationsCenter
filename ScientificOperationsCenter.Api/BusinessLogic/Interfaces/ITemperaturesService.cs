using ScientificOperationsCenter.Api.BusinessLogic.Structs;


namespace ScientificOperationsCenter.Api.BusinessLogic.Interfaces
{
    public interface ITemperaturesService
    {
        Task<IEnumerable<TemperaturesTimeAverage>> GetAverageTemperaturesForTheDayAsync(DateOnly date);


        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheMonthAsync(DateOnly date);


        Task<IEnumerable<TemperaturesDateAverage>> GetAverageTemperaturesForTheYearAsync(DateOnly date);
    }
}
