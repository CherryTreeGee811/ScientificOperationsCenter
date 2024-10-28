using ScientificOperationsCenter.Api.ViewModels;

namespace ScientificOperationsCenter.Api.Mappers.Interfaces
{
    public interface IRadiationMeasurementsMapper
    {
        Task<IEnumerable<RadiationMeasurementsTimeViewModel>> GetRadiationMeasurementsForTheDayAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheMonthAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheYearAsync(DateOnly date);
    }
}
