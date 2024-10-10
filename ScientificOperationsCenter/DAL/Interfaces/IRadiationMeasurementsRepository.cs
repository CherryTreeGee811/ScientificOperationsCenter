using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL.Interfaces
{
    public interface IRadiationMeasurementsRepository
    {
        IEnumerable<RadiationMeasurements> GetAll();


        public IEnumerable<RadiationMeasurements> GetByDay(DateOnly date);


        public IEnumerable<RadiationMeasurements> GetByMonth(DateOnly date);


        public IEnumerable<RadiationMeasurements> GetByYear(DateOnly date);
    }
}
