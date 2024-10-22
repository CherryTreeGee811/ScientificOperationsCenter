using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL.Interfaces
{
    public interface IRadiationMeasurementsRepository
    {
        Task<IEnumerable<RadiationMeasurements>> GetByDayAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurements>> GetByMonthAsync(DateOnly date);


        Task<IEnumerable<RadiationMeasurements>> GetByYearAsync(DateOnly date);
    }
}
