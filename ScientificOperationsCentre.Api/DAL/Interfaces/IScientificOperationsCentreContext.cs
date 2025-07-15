using Microsoft.EntityFrameworkCore;
using ScientificOperationsCentre.Api.Models;


namespace ScientificOperationsCentre.Api.DAL.Interfaces
{
    /// <summary>
    /// Defines the contract for the Scientific Operations Centre database context.
    /// </summary>
    public interface IScientificOperationsCentreContext
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
