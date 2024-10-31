using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    /// <summary>
    /// Defines the contract for the Scientific Operations Center database context.
    /// </summary>
    public interface IScientificOperationsCenterContext
    {
        /// <summary>
        /// Gets the DbSet for temperature records.
        /// </summary>
        public DbSet<Temperatures> Temperatures { get; }


        /// <summary>
        /// Gets the DbSet for radiation measurement records.
        /// </summary>
        public DbSet<RadiationMeasurements> RadiationMeasurements { get; }
    }
}
