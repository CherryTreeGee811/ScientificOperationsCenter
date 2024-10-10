using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface IRadiationMeasurementsService
    {
        IEnumerable<RadiationMeasurementTimeSums> GetRadiationMeasurementsSumForTheDay(DateOnly date);


        IEnumerable<RadiationMeasurementDateSums> GetRadiationMeasurementsSumForTheMonth(DateOnly date);


        IEnumerable<RadiationMeasurementDateSums> GetRadiationMeasurementsSumForTheYear(DateOnly date);
    }
}
