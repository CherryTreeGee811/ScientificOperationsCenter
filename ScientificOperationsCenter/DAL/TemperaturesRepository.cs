using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.DAL.Interfaces;
using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL
{
    public sealed class TemperaturesRepository : ITemperaturesRepository
    {
        private readonly DbSet<Temperatures> _dbSet;


        public TemperaturesRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<Temperatures>();
        }


        public IEnumerable<Temperatures> GetByDay(DateOnly date)
        {
            IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
            return query.ToList();
        }


        public IEnumerable<Temperatures> GetByMonth(DateOnly date)
        {
            IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
            return query.ToList();
        }


        public IEnumerable<Temperatures> GetByYear(DateOnly date)
        {
            IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year);
            return query.ToList();
        }
    }
}
