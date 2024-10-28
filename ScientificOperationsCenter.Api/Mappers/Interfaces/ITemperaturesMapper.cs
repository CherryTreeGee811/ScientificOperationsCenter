using ScientificOperationsCenter.Api.ViewModels;

namespace ScientificOperationsCenter.Api.Mappers.Interfaces
{
    public interface ITemperaturesMapper
    {
        Task<IEnumerable<TemperaturesTimeViewModel>> GetTemperaturesForTheDayAsync(DateOnly date);


       Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheMonthAsync(DateOnly date);


       Task<IEnumerable<TemperaturesDateViewModel>> GetTemperaturesForTheYearAsync(DateOnly date);
    }
}
