using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.DAL
{
    public sealed class TemperaturesRepository : ITemperaturesRepository
    {
        private readonly DbSet<Temperatures> _dbSet;


        public TemperaturesRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<Temperatures>();
        }


        public async Task<IEnumerable<Temperatures>> GetByDayAsync(DateOnly date)
        {
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in TemperaturesRepo -> GetByDayAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in TemperaturesRepo -> GetByDayAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesRepo -> GetByDayAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public async Task<IEnumerable<Temperatures>> GetByMonthAsync(DateOnly date)
        {
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in TemperaturesRepo -> GetByMonthAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in TemperaturesRepo -> GetByMonthAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesRepo -> GetByMonthAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public async Task<IEnumerable<Temperatures>> GetByYearAsync(DateOnly date)
        {
            try
            {
                IQueryable<Temperatures> query = _dbSet.Where(x => x.Date.Year == date.Year);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in TemperaturesRepo -> GetByYearAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in TemperaturesRepo -> GetByYearAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesRepo -> GetByYearAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }
    }
}