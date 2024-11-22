using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.CustomExceptions;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.DAL
{
    /// <summary>
    /// Manages initial data access for temperatures.
    /// Filters data to meet the requirements of the ScientificOperationsCenter application.
    /// Represents data retrieved from the database using the Temperatures data model.
    /// </summary>
    public sealed class TemperaturesRepository : ITemperaturesRepository
    {
        // ToDo: Update comments + Tests

        /// <summary>
        /// Represents the Temperatures database table.
        /// </summary>
        private readonly DbSet<Temperatures> _dbSet;
        private readonly ScientificOperationsCenterContext _context;


        /// <summary>
        /// Initializes a new instance of the <see cref="TemperaturesRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used to access the Temperatures database table.</param>
        public TemperaturesRepository(ScientificOperationsCenterContext context)
        {
            _dbSet = context.Set<Temperatures>();
            _context = context;
        }


        /// <summary>
        /// Retrieves all temperatures from the database for a specific day,
        /// where the year, month, and day match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified day. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        /// <summary>
        /// Retrieves all temperatures records from the database for a specific month,
        /// where the year and month match the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified month. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        /// <summary>
        /// Retrieves all temperature records from the database for the year 
        /// corresponding to the provided date.
        /// </summary>
        /// <param name="date">A valid date in the format YYYY-MM-DD provided by the user.</param>
        /// <returns>
        /// A collection of <see cref="Temperatures"/> for the specified year. 
        /// If no temperatures are found, an empty collection is returned.
        /// </returns>
        /// <exception cref="DataAccessException">
        /// Thrown when there is a problem with data access.
        /// </exception>
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


        public async Task AddTemperatures(Temperatures[] temperatureList)
        {
            try
            {
                _context.Temperatures.AddRange(temperatureList);
                await _context.SaveChangesAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in TemperaturesRepo -> AddTemperatures().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in TemperaturesRepo -> AddTemperatures().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesRepo -> AddTemperatures().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }


        public async Task AddTemperature(Temperatures temperature)
        {
            try
            {
                _context.Temperatures.Add(temperature);
                await _context.SaveChangesAsync();
            }
            catch (SqlException dbEx)
            {
                Log.Error(dbEx, "An SqlException was thrown in TemperaturesRepo -> AddTemperature().");
                throw new DataAccessException("An error occurred while accessing the database.", dbEx);
            }
            catch (InvalidOperationException iEx)
            {
                Log.Error(iEx, "An InvalidOperationException was thrown in TemperaturesRepo -> AddTemperature().");
                throw new DataAccessException("An error occurred while accessing the database.", iEx);
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "An unexpected error occurred in TemperaturesRepo -> AddTemperature().");
                throw new DataAccessException("An unexpected error occurred.", gEx);
            }
        }
    }
}