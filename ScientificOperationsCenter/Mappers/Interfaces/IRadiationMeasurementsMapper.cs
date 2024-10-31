using ScientificOperationsCenter.ViewModels;

namespace ScientificOperationsCenter.Mappers.Interfaces
{
    public interface IRadiationMeasurementsMapper
    {
        public IEnumerable<RadiationMeasurementsTimeViewModel> GetRadiationMeasurementsForTheDay(DateOnly date);


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheMonth(DateOnly date);


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheYear(DateOnly date);
    }
}
