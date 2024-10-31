using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Mappers.Interfaces;
using ScientificOperationsCenter.ViewModels;


namespace ScientificOperationsCenter.Mappers
{
    public sealed class RadiationMeasurementsMapper : IRadiationMeasurementsMapper
    {
        private readonly IRadiationMeasurementsService _service;


        public RadiationMeasurementsMapper(IRadiationMeasurementsService service)
        {
            _service = service;
        }


        public IEnumerable<RadiationMeasurementsTimeViewModel> GetRadiationMeasurementsForTheDay(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheDay(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Time.Hour).Select(r =>
                    new RadiationMeasurementsTimeViewModel { Hour = r.Time, TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return Enumerable.Empty<RadiationMeasurementsTimeViewModel>();
        }


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheMonth(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheMonth(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Date.Day).Select(r =>
                    new RadiationMeasurementsDateViewModel { Date = r.Date.Day.ToString(), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return Enumerable.Empty<RadiationMeasurementsDateViewModel>();
        }


        public IEnumerable<RadiationMeasurementsDateViewModel> GetRadiationMeasurementsForTheYear(DateOnly date)
        {
            var radiationMeasurements = _service.GetRadiationMeasurementsSumForTheYear(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Date.Month).Select(r =>
                    new RadiationMeasurementsDateViewModel { Date = r.Date.ToString("MMMM"), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return Enumerable.Empty<RadiationMeasurementsDateViewModel>();
        }
    }
}
