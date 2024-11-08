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
        /// Gets or sets the DbSet for temperature records.
        /// </summary>
        DbSet<Temperatures> Temperatures { get; set; }


        /// <summary>
        /// Gets or sets the DbSet for radiation measurement records.
        /// </summary>
        DbSet<RadiationMeasurements> RadiationMeasurements { get; set; }
    }
}
