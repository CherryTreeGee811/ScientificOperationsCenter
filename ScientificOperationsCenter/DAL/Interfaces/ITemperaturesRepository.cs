using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL.Interfaces
{
    public interface ITemperaturesRepository
    {
        IEnumerable<Temperatures> GetAll();


        public IEnumerable<Temperatures> GetByDay(DateOnly date);


        public IEnumerable<Temperatures> GetByMonth(DateOnly date);


        public IEnumerable<Temperatures> GetByYear(DateOnly date);
    }
}
