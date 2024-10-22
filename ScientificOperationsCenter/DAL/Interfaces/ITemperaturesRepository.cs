using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL.Interfaces
{
    public interface ITemperaturesRepository
    {
        Task<IEnumerable<Temperatures>> GetByDayAsync(DateOnly date);


        Task<IEnumerable<Temperatures>> GetByMonthAsync(DateOnly date);


        Task<IEnumerable<Temperatures>> GetByYearAsync(DateOnly date);
    }
}
