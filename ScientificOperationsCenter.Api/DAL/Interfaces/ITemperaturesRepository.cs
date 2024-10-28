using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    public interface ITemperaturesRepository
    {
        Task<IEnumerable<Temperatures>> GetByDayAsync(DateOnly date);


        Task<IEnumerable<Temperatures>> GetByMonthAsync(DateOnly date);


        Task<IEnumerable<Temperatures>> GetByYearAsync(DateOnly date);
    }
}
