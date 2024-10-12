using Microsoft.Data.SqlClient;
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
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
                return query.ToList();
            }
            catch (SqlException dbEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public IEnumerable<Temperatures> GetByMonth(DateOnly date)
        {
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
                return query.ToList();
            }
            catch (SqlException dbEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public IEnumerable<Temperatures> GetByYear(DateOnly date)
        {
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year);
                return query.ToList();
            }
            catch (SqlException dbEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                // Todo: Log Exception
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }
    }
}
