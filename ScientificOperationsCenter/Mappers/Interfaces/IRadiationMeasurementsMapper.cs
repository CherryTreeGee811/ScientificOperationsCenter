using ScientificOperationsCenter.ViewModels;

namespace ScientificOperationsCenter.Mappers.Interfaces
{
    public interface IRadiationMeasurementsMapper
    {
        Task<IEnumerable<RadiationMeasurementsTimeViewModel>> GetRadiationMeasurementsForTheDayAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheMonthAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheYearAsync(DateOnly date);
    }
}
