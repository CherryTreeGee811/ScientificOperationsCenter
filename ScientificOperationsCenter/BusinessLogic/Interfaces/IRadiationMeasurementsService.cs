using ScientificOperationsCenter.BusinessLogic.Structs;


namespace ScientificOperationsCenter.BusinessLogic.Interfaces
{
    public interface IRadiationMeasurementsService
    {
        IEnumerable<RadiationMeasurementsTimeSum> GetRadiationMeasurementsSumForTheDay(DateOnly date);


        IEnumerable<RadiationMeasurementsDateSum> GetRadiationMeasurementsSumForTheMonth(DateOnly date);


        IEnumerable<RadiationMeasurementsDateSum> GetRadiationMeasurementsSumForTheYear(DateOnly date);
    }
}
