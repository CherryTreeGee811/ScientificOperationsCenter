using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface IRadiationMeasurementsService
    {
        Task<IEnumerable<RadiationMeasurementsTimeSum>> GetRadiationMeasurementsSumForTheDayAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheMonthAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurementsDateSum>> GetRadiationMeasurementsSumForTheYearAsync(DateOnly date);
    }
}
