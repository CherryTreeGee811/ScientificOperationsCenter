using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.DAL
{
    // ToDo: Update comments + tests

    /// <summary>
    /// Manages initial data access for radiation measurements.
    /// Filters data to meet the requirements of the ScientificOperationsCenter application.
    /// Represents data retrieved from the database using the RadiationMeasurements data model.
    /// </summary>
    public sealed class RadiationMeasurementsRepository : IRadiationMeasurementsRepository
    {
        /// <summary>
        /// Represents the Radiation Measurement database table.
        /// </summary>
        private readonly DbSet<RadiationMeasurements> _dbSet;
        private readonly ScientificOperationsCenterContext _context;


        /// <summary>
        /// Initializes a new instance of the <see cref="RadiationMeasurementsRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used to access the Radiation Measurements database table.</param>
        public RadiationMeasurementsRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<RadiationMeasurements>();
            _context = context;
        }


        /// <summary>
        /// Retrieves all radiation measurements from the database for a specific day,
        /// where the year, month, and day match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified day. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        /// <summary>
        /// Retrieves all radiation measurements from the database for a specific month,
        /// where the year and month match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified month. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        /// <summary>
        /// Retrieves all radiation measurements from the database for the year 
        /// corresponding to the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="RadiationMeasurements"/> for the specified year. 
        /// If no measurements are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        public async Task AddRadiationMeasurements(RadiationMeasurements[] radiationMeasurementsList)
        {
            try
            {
                _context.RadiationMeasurements.AddRange(radiationMeasurementsList);
                await _context.SaveChangesAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in RadiationMeasurementsRepo -> AddRadiationMeasurements().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in RadiationMeasurementsRepo -> AddRadiationMeasurements().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in RadiationMeasurementsRepo -> AddRadiationMeasurements().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }
    }
}