using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using ScientificOperationsCenter.Api.ViewModels;


namespace ScientificOperationsCenter.Api.Mappers
{
    public sealed class RadiationMeasurementsMapper : IRadiationMeasurementsMapper
    {
        private readonly IRadiationMeasurementsService _service;


        public RadiationMeasurementsMapper(IRadiationMeasurementsService service)
        {
            _service = service;
        }


        public async Task<IEnumerable<RadiationMeasurementsTimeViewModel>> GetRadiationMeasurementsForTheDayAsync(DateOnly date)
        {
            var radiationMeasurements = await _service.GetRadiationMeasurementsSumForTheDayAsync(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Time.Hour).Select(r =>
                    new RadiationMeasurementsTimeViewModel { Hour = r.Time, TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return Enumerable.Empty<RadiationMeasurementsTimeViewModel>();
        }


        public async Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheMonthAsync(DateOnly date)
        {
            var radiationMeasurements = await _service.GetRadiationMeasurementsSumForTheMonthAsync(date);
            if (radiationMeasurements.Any())
            {
                var values = radiationMeasurements.OrderBy(t => t.Date.Day).Select(r =>
                    new RadiationMeasurementsDateViewModel { Date = r.Date.Day.ToString(), TotalRadiation = r.TotalMilligrays });
                return values;
            }
            return Enumerable.Empty<RadiationMeasurementsDateViewModel>();
        }


        public async Task<IEnumerable<RadiationMeasurementsDateViewModel>> GetRadiationMeasurementsForTheYearAsync(DateOnly date)
        {
            var radiationMeasurements = await _service.GetRadiationMeasurementsSumForTheYearAsync(date);
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
