using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.DAL.Interfaces;
using ScientificOperationsCenter.Models;


namespace ScientificOperationsCenter.DAL
{
    public sealed class RadiationMeasurementsRepository : IRadiationMeasurementsRepository
    {
        private readonly DbSet<RadiationMeasurements> _dbSet;


        public RadiationMeasurementsRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<RadiationMeasurements>();
        }


        public IEnumerable<RadiationMeasurements> GetByDay(DateOnly date)
        {
            IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
            return query.ToList();
        }


        public IEnumerable<RadiationMeasurements> GetByMonth(DateOnly date)
        {
            IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
            return query.ToList();
        }


        public IEnumerable<RadiationMeasurements> GetByYear(DateOnly date)
        {
            IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year);
            return query.ToList();
        }
    }
}