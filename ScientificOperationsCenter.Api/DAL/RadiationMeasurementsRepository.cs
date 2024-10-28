using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.DAL
{
    public sealed class RadiationMeasurementsRepository : IRadiationMeasurementsRepository
    {
        private readonly DbSet<RadiationMeasurements> _dbSet;


        public RadiationMeasurementsRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<RadiationMeasurements>();
        }


        public async Task<IEnumerable<RadiationMeasurements>> GetByDayAsync(DateOnly date)
        {
            try
            {
                IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SQLException was thrown in RadiationMeasurementsRepo -> GetByDayAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An DataAccessException was thrown in RadiationMeasurementsRepo -> GetByDayAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsRepo -> GetByDayAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public async Task<IEnumerable<RadiationMeasurements>> GetByMonthAsync(DateOnly date)
        {
            try
            {
                IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in RadiationMeasurementsRepo -> GetByMonthAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in RadiationMeasurementsRepo -> GetByMonthAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsRepo -> GetByMonthAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public async Task<IEnumerable<RadiationMeasurements>> GetByYearAsync(DateOnly date)
        {
            try
            {
                IQueryable<RadiationMeasurements> query = _dbSet.Where(x => x.Date.Year == date.Year);
                return await query.ToListAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in RadiationMeasurementsRepo -> GetByYearAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
              catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in RadiationMeasurementsRepo -> GetByYearAsync().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsRepo -> GetByYearAsync().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }
    }
}