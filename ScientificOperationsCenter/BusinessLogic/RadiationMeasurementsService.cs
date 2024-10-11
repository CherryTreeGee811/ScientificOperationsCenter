using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.DAL.Interfaces;


namespace ScientificOperationsCenter.BusinessLogic
{
    public class RadiationMeasurementsService : IRadiationMeasurementsService
    {

        private IRadiationMeasurementsRepository _radiationMeasurementsRepository;


        public RadiationMeasurementsService(IRadiationMeasurementsRepository radiationMeasurementsRepository)
        {
            _radiationMeasurementsRepository = radiationMeasurementsRepository;
        }


        public IEnumerable<RadiationMeasurementTimeSums> GetRadiationMeasurementsSumForTheDay(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByDay(date);
            IEnumerable<RadiationMeasurementTimeSums> values = radiationMeasurements.GroupBy(t => t.Time.Hour)
                .Select(r => new RadiationMeasurementTimeSums { Time = new TimeOnly(r.Key, 00), TotalMilligrays = r.Sum(a => a.Milligrays) }).OrderBy(t => t.Time.Hour);

            return values;
        }


        public IEnumerable<RadiationMeasurementDateSums> GetRadiationMeasurementsSumForTheMonth(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByMonth(date);
            IEnumerable<RadiationMeasurementDateSums> values = radiationMeasurements.GroupBy(t => t.Date.Day)
                .Select(r => new RadiationMeasurementDateSums { Date = new DateOnly(date.Year, date.Month, r.Key), TotalMilligrays = r.Sum(a => a.Milligrays) }).OrderBy(t => t.Date.Day);
            return values;
        }


        public IEnumerable<RadiationMeasurementDateSums> GetRadiationMeasurementsSumForTheYear(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByYear(date);
            IEnumerable<RadiationMeasurementDateSums> values = radiationMeasurements.GroupBy(t => t.Date.Month)
                .Select(r => new RadiationMeasurementDateSums { Date = new DateOnly(date.Year, r.Key, 01), TotalMilligrays = r.Sum(a => a.Milligrays) }).OrderBy(t => t.Date.Month);
            return values;
        }
    }
}
