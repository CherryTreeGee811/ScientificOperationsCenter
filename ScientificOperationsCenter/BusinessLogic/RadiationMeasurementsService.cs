using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Structs;
using ScientificOperationsCenter.DAL.Interfaces;


namespace ScientificOperationsCenter.BusinessLogic
{
    public sealed class RadiationMeasurementsService : IRadiationMeasurementsService
    {

        private readonly IRadiationMeasurementsRepository _radiationMeasurementsRepository;


        public RadiationMeasurementsService(IRadiationMeasurementsRepository radiationMeasurementsRepository)
        {
            _radiationMeasurementsRepository = radiationMeasurementsRepository;
        }


        public IEnumerable<RadiationMeasurementsTimeSum> GetRadiationMeasurementsSumForTheDay(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByDay(date);
            if(radiationMeasurements.Any())
            {
                try
                {
                    var values = radiationMeasurements.GroupBy(t => t.Time.Hour)
                        .Select(r => new RadiationMeasurementsTimeSum { Time = new TimeOnly(r.Key, 00), TotalMilligrays = r.Sum(a => a.Milligrays) });
                    return values;
                }
                catch (Exception gEx)
                {
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<RadiationMeasurementsTimeSum>();
        }


        public IEnumerable<RadiationMeasurementsDateSum> GetRadiationMeasurementsSumForTheMonth(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByMonth(date);
            if (radiationMeasurements.Any())
            {
                try
                {
                    var values = radiationMeasurements.GroupBy(t => t.Date.Day)
                        .Select(r => new RadiationMeasurementsDateSum { Date = new DateOnly(date.Year, date.Month, r.Key), TotalMilligrays = r.Sum(a => a.Milligrays) });
                    return values;
                }
                catch (Exception gEx)
                {
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }

            }
            return Enumerable.Empty<RadiationMeasurementsDateSum>();
        }


        public IEnumerable<RadiationMeasurementsDateSum> GetRadiationMeasurementsSumForTheYear(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsRepository.GetByYear(date);
            if (radiationMeasurements.Any())
            {
                try
                {
                    var values = radiationMeasurements.GroupBy(t => t.Date.Month)
                        .Select(r => new RadiationMeasurementsDateSum { Date = new DateOnly(date.Year, r.Key, 01), TotalMilligrays = r.Sum(a => a.Milligrays) });
                    return values;
                }
                catch (Exception gEx)
                {
                    // Todo: Log Exception
                    throw new BusinessLogicException("An unexpected error occurred.", gEx);
                }
            }
            return Enumerable.Empty<RadiationMeasurementsDateSum>();
        }
    }
}
