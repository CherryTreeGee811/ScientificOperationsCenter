using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic.Structs;
using ScientificOperationsCenter.Api.DAL.Interfaces;


namespace ScientificOperationsCenter.Api.BusinessLogic
{
    public sealed class RadiationMeasurementsService : IRadiationMeasurementsService
    {

        private readonly IRadiationMeasurementsRepository _radiationMeasurementsRepository;


        public RadiationMeasurementsService(IRadiationMeasurementsRepository radiationMeasurementsRepository)
        {
            _radiationMeasurementsRepository = radiationMeasurementsRepository;
        }


        public async Task<IEnumerable<RadiationMeasurementsTimeSum>> GetRadiationMeasurementsSumForTheDayAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByDayAsync(date);
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


        public async Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheMonthAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByMonthAsync(date);
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


        public async Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheYearAsync(DateOnly date)
        {
            var radiationMeasurements = await _radiationMeasurementsRepository.GetByYearAsync(date);
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
