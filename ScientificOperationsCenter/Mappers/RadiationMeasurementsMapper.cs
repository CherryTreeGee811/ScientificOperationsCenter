using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Mappers.Interfaces;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Mappers
{
    public class RadiationMeasurementsMapper : IRadiationMeasurementsMapper
    {
        IRadiationMeasurementsService _service;


        public RadiationMeasurementsMapper(IRadiationMeasurementsService service)
        {
            _service = service;
        }


        public IEnumerable<RadiationMeasurementsTimeViewModel> GetRadiationMeasurementsForTheDay(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheDay(date);
            IEnumerable<RadiationMeasurementsTimeViewModel> values = radiationMeasurements.Select(r => new RadiationMeasurementsTimeViewModel { Hour = r.Time, TotalRadiation = r.TotalMilligrays });
            return values;
        }


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheMonth(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheMonth(date);
            IEnumerable<RadiationMeasurementsDateViewModel> values = radiationMeasurements.Select(r => new RadiationMeasurementsDateViewModel { Date = r.Date.DayNumber.ToString(), TotalRadiation = r.TotalMilligrays });
            return values;
        }


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheYear(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheYear(date);
            IEnumerable<RadiationMeasurementsDateViewModel> values = radiationMeasurements.Select(r => new RadiationMeasurementsDateViewModel { Date = r.Date.ToString("MMMM"), TotalRadiation = r.TotalMilligrays });
            return values;
        }
    }
}
