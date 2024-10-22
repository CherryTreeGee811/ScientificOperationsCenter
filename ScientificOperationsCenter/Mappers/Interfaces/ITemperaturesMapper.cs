using ScientificOperationsCenter.ViewModels;

namespace ScientificOperationsCenter.Mappers.Interfaces
{
    public interface ITemperaturesMapper
    {
        Task<IEnumerable<TemperaturesTimeViewModel>> GetTemperaturesForTheDayAsync(DateOnly date);


       Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheMonthAsync(DateOnly date);


       Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheYearAsync(DateOnly date);
    }
}
